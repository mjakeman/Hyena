//
// TextBlock.cs
//
// Author:
//       Aaron Bockover <abockover@novell.com>
//
// Copyright 2009 Aaron Bockover
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Linq;

using Cairo;
using Hyena.Gui;

namespace Hyena.Gui.Canvas
{
    public class TextBlock : CanvasItem
    {
        private Pango.Layout layout;
        private Rect text_alloc = Rect.Empty;
        private Rect invalidation_rect = Rect.Empty;

        public TextBlock ()
        {
            FontWeight = FontWeight.Normal;
            TextWrap = TextWrap.None;
            EllipsizeMode = Pango.EllipsizeMode.End;
        }

        private bool EnsureLayout ()
        {
            if (layout != null) {
                return true;
            }

            Gtk.Widget widget = Manager == null ? null : Manager.Host as Gtk.Widget;
            if (widget == null || widget.GdkWindow == null || !widget.IsRealized) {
                return false;
            }

            using (var cr = Gdk.CairoHelper.Create (widget.GdkWindow)) {
                layout = CairoExtensions.CreateLayout (widget, cr);
            }

            return layout != null;
        }

        public override Size Measure (Size available)
        {
            if (!EnsureLayout ()) {
                return new Size (0, 0);
            }

            available = base.Measure (available);

            int text_w, text_h;

            TextWrap wrap = TextWrap;
            layout.Width = wrap == TextWrap.None ? -1 : (int)(Pango.Scale.PangoScale * (available.Width - Margin.X));
            layout.Wrap = GetPangoWrapMode (wrap);
            layout.FontDescription.Weight = GetPangoFontWeight (FontWeight);
            layout.SingleParagraphMode = wrap == TextWrap.None;
            layout.Ellipsize = EllipsizeMode;
            
            last_text = GetFormattedText (GetText ()) ?? "";
            if (TextWrap == TextWrap.None && last_text.IndexOfAny (lfcr) >= 0) {
                last_text = last_text.Replace ("\r\n", "\x20").Replace ('\n', '\x20').Replace ('\r', '\x20');
            }

            if (UseMarkup) {
                layout.SetMarkup (last_text);
            } else {
                layout.SetText (last_text);
            }

            layout.GetPixelSize (out text_w, out text_h);

            double width = text_w;
            if (!available.IsEmpty && available.Width > 0) {
                width = available.Width;
            }

            //DesiredSize = new Size (width, text_h);
            var size = new Size (width, text_h);

            // Hack, as this prevents the TextBlock from
            // being flexible in a Vertical StackPanel
            Height = size.Height;

            if (ForceSize) {
                Width = DesiredSize.Width;
            }

            return size;
        }

        private string GetText ()
        {
            if (TextGenerator != null) {
                return TextGenerator (BoundObject);
            } else {
                var so = BoundObject;
                return so == null ? Text : so.ToString ();
            }
        }

        private string GetFormattedText (string text)
        {
            if (String.IsNullOrEmpty (TextFormat)) {
                return text;
            }
            return String.Format (TextFormat, UseMarkup ? GLib.Markup.EscapeText (text) : text);
        }

        public override void Arrange ()
        {
            if (!EnsureLayout ()) {
                return;
            }

            //int layout_width = TextWrap == TextWrap.None ? -1 : (int)(Pango.Scale.PangoScale * RenderSize.Width);
            int layout_width = (int)(Pango.Scale.PangoScale * RenderSize.Width);
            if (layout.Width != layout_width) {
                layout.Width = layout_width;
            }

            int text_width, text_height;
            if (TextWrap != TextWrap.None) {
                layout.SetHeight ((int)(Pango.Scale.PangoScale * RenderSize.Height));
            }
            layout.GetPixelSize (out text_width, out text_height);

            if (layout.IsEllipsized || text_width > RenderSize.Width || text_height > RenderSize.Height) {
                TooltipMarkup = last_text;
            } else {
                TooltipMarkup = null;
            }

            Rect new_alloc = new Rect (
                Math.Round ((RenderSize.Width - text_width) * HorizontalAlignment),
                Math.Round ((RenderSize.Height - text_height) * VerticalAlignment),
                text_width,
                text_height);

            if (text_alloc.IsEmpty) {
                InvalidateRender (text_alloc);
            } else {
                invalidation_rect = text_alloc;
                invalidation_rect.Union (new_alloc);

                // Some padding, likely because of the pen size for
                // showing the actual text layout in the render pass
                invalidation_rect.X -= 2;
                invalidation_rect.Y -= 2;
                invalidation_rect.Width += 4;
                invalidation_rect.Height += 4;

                InvalidateRender (invalidation_rect);
            }

            text_alloc = new_alloc;
        }

        protected override void ClippedRender (Context cr)
        {
            if (!EnsureLayout ()) {
                return;
            }

            Brush foreground = Foreground;
            if (!foreground.IsValid) {
                return;
            }

            cr.Rectangle (0, 0, RenderSize.Width, RenderSize.Height);
            cr.Clip ();

            bool fade = Fade && text_alloc.Width > RenderSize.Width;

            if (fade) {
                cr.PushGroup ();
            }

            cr.MoveTo (text_alloc.X, text_alloc.Y);
            Foreground.Apply (cr);
            Pango.CairoHelper.ShowLayout (cr, layout);
            cr.Fill ();

            if (fade) {
                LinearGradient mask = new LinearGradient (RenderSize.Width - 20, 0, RenderSize.Width, 0);
                mask.AddColorStop (0, new Color (0, 0, 0, 1));
                mask.AddColorStop (1, new Color (0, 0, 0, 0));

                cr.PopGroupToSource ();
                cr.Mask (mask);
                mask.Destroy ();
            }

            cr.ResetClip ();
        }

        private Pango.Weight GetPangoFontWeight (FontWeight weight)
        {
            switch (weight) {
                case FontWeight.Bold: return Pango.Weight.Bold;
                default: return Pango.Weight.Normal;
            }
        }

        private Pango.WrapMode GetPangoWrapMode (TextWrap wrap)
        {
            switch (wrap) {
                case TextWrap.Char: return Pango.WrapMode.Char;
                case TextWrap.WordChar: return Pango.WrapMode.WordChar;
                case TextWrap.None:
                case TextWrap.Word:
                default:
                    return Pango.WrapMode.Word;
            }
        }

        protected override Rect InvalidationRect {
            get { return invalidation_rect; }
        }

        public override string ToString ()
        {
            return String.Format ("<TextBlock Text='{0}' Allocation={1}>", last_text, Allocation);
        }

        public string Text { get; set; }
        public string TextFormat { get; set; }
        public FontWeight FontWeight { get; set; }
        public TextWrap TextWrap { get; set; }
        public bool Fade { get; set; }
        public bool ForceSize { get; set; }
        public Pango.EllipsizeMode EllipsizeMode { get; set; }
        public Func<object, string> TextGenerator { get; set; }
        public bool UseMarkup { get; set; }

        public double HorizontalAlignment { get; set; }
        public double VerticalAlignment { get; set; }

        private static char[] lfcr = new char[] {'\n', '\r'};
        private string last_text;
    }
}

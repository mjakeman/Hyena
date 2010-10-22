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
            InstallProperty<string> ("Text", String.Empty);
            InstallProperty<string> ("TextFormat", "");
            InstallProperty<bool> ("UseMarkup", false);
            InstallProperty<double> ("HorizontalAlignment", 0.0);
            InstallProperty<double> ("VerticalAlignment", 0.0);
            InstallProperty<FontWeight> ("FontWeight", FontWeight.Normal);
            InstallProperty<TextWrap> ("TextWrap", TextWrap.None);
            InstallProperty<bool> ("ForceSize", false);
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
            layout.Width = wrap == TextWrap.None ? -1 : (int)(Pango.Scale.PangoScale * available.Width);
            layout.Wrap = GetPangoWrapMode (wrap);
            layout.FontDescription.Weight = GetPangoFontWeight (FontWeight);
            layout.SingleParagraphMode = wrap == TextWrap.None;
            layout.Ellipsize = EllipsizeMode;
            UpdateLayout (layout, GetText ());
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

        private static char[] lfcr = new char[] {'\n', '\r'};
        private void UpdateLayout (Pango.Layout layout, string text)
        {
            string final_text = GetFormattedText (text);
            if (TextWrap == TextWrap.None && final_text.IndexOfAny (lfcr) >= 0) {
                final_text = final_text.Replace ("\r\n", "\x20").Replace ('\n', '\x20').Replace ('\r', '\x20');
            }

            if (UseMarkup) {
                layout.SetMarkup (final_text);
            } else {
                layout.SetText (final_text);
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

            int layout_width = TextWrap == TextWrap.None
                ? -1
                : (int)(Pango.Scale.PangoScale * ContentAllocation.Width);
            if (layout.Width != layout_width) {
                layout.Width = layout_width;
            }

            int text_width, text_height;
            layout.SetHeight ((int)(Pango.Scale.PangoScale * RenderSize.Height));
            layout.GetPixelSize (out text_width, out text_height);

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

        protected override bool OnPropertyChange (string property, object value)
        {
            switch (property) {
                case "FontWeight":
                case "TextWrap":
                case "Text":
                case "TextFormat":
                case "UseMarkup":
                case "ForceSize":
                    if (layout != null) {
                        InvalidateMeasure ();
                        InvalidateArrange ();
                    }
                    return true;
                case "HorizontalAlignment":
                case "VerticalAlignment":
                    if (layout != null) {
                        InvalidateArrange ();
                    }
                    return true;
            }

            return base.OnPropertyChange (property, value);
        }

        protected override Rect InvalidationRect {
            get { return invalidation_rect; }
        }

        public string Text {
            get { return GetValue<string> ("Text"); }
            set { SetValue<string> ("Text", value); }
        }

        public string TextFormat {
            get { return GetValue<string> ("TextFormat"); }
            set { SetValue<string> ("TextFormat", value); }
        }

        public FontWeight FontWeight {
            get { return GetValue<FontWeight> ("FontWeight"); }
            set { SetValue<FontWeight> ("FontWeight", value); }
        }

        public TextWrap TextWrap {
            get { return GetValue<TextWrap> ("TextWrap"); }
            set { SetValue<TextWrap> ("TextWrap", value); }
        }

        public bool Fade { get; set; }

        public bool ForceSize {
            get { return GetValue<bool> ("ForceSize"); }
            set { SetValue<bool> ("ForceSize", value); }
        }

        public virtual Pango.EllipsizeMode EllipsizeMode { get; set; }

        public Func<object, string> TextGenerator { get; set; }

        public bool UseMarkup {
            get { return GetValue<bool> ("UseMarkup"); }
            set { SetValue<bool> ("UseMarkup", value); }
        }

        public double HorizontalAlignment {
            get { return GetValue<double> ("HorizontalAlignment", 0.0); }
            set { SetValue<double> ("HorizontalAlignment", value); }
        }

        public double VerticalAlignment {
            get { return GetValue<double> ("VerticalAlignment", 0.0); }
            set { SetValue<double> ("VerticalAlignment", value); }
        }
    }
}

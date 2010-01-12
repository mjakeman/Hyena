//
// ImageButton.cs
//
// Authors:
//   Gabriel Burt <gburt@novell.com>
//
// Copyright (C) 2008 Novell, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using Gtk;

namespace Hyena.Widgets
{
    public class ImageButton : Button
    {
        public ImageButton (string text, string iconName) : this (text, iconName, Gtk.IconSize.Button)
        {
        }

        public ImageButton (string text, string iconName, Gtk.IconSize iconSize) : base ()
        {
            Image image = new Image ();
            image.IconName = iconName;
            image.IconSize = (int) iconSize;

            Label label = new Label ();
            label.MarkupWithMnemonic = text;

            HBox hbox = new HBox ();
            hbox.Spacing = 2;
            hbox.PackStart (image, false, false, 0);
            hbox.PackStart (label, true, true, 0);

            Child = hbox;
            CanDefault = true;
            ShowAll ();
        }
    }
}
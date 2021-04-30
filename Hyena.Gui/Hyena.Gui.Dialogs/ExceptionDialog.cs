//
// ExceptionDialog.cs
//
// Author:
//   Aaron Bockover <abockover@novell.com>
//   Matthew Jakeman <mjakeman26@outlook.co.nz>
//
// Copyright (C) 2005-2007 Novell, Inc.
//               2020 Matthew Jakeman
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
using System.Reflection;
using System.Resources;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using Gtk;

namespace Hyena.Gui.Dialogs
{
    public class ExceptionDialog : Dialog
    {
        private string debugInfo;

        // Widgets
        private Box vbox;

        public ExceptionDialog(Exception e)
        {
            debugInfo = BuildExceptionMessage(e);

            Resizable = false;
            SetBorderWidth(5);

            this.AddHyenaStyleClass("hyena-exception-dialog");

            // Translators: {0} is substituted with the application name
            Title = String.Format(Catalog.GetString("{0} Encountered a Fatal Error"),
                                  ApplicationContext.ApplicationName);

            // TODO: Add Accelerators
            // TODO: Escape Markup Functions

            // Create GUI
            vbox = (Box)GetContentArea();
            vbox.Spacing = 12;

            // Header
            var hbox = Box.New(Orientation.Horizontal, 12);
            vbox.PackStart(hbox, false, false, 0);
            
            var image = Image.NewFromIconName("dialog-error", IconSize.Dialog);
            image.Valign = Align.Start;
            hbox.PackStart(image, false, false, 0);

            Box labelVbox = Box.New(Orientation.Vertical, 12);
            hbox.PackStart(labelVbox, true, true, 0);

            Label label = Label.New($"<b><big>{Title}</big></b>");
            label.UseMarkup = true;
            label.Justify = Justification.Left;
            label.Wrap = true;
            label.Xalign = 0;
            labelVbox.PackStart(label, false, false, 0);

            label = Label.New(e.Message);

            label.UseMarkup = true;
            label.UseUnderline = false;
            label.Justify = Justification.Left;
            label.Wrap = true;
            label.Selectable = true;
            label.Xalign = 0;
            labelVbox.PackStart(label, false, false, 0);

            Label detailsLabel = Label.New($"<b>{Catalog.GetString("Error Details")}</b>");
            detailsLabel.UseMarkup = true;

            Expander detailsExpander = Expander.New("Details");
            detailsExpander.LabelWidget = detailsLabel;
            detailsExpander.ResizeToplevel = true;
            labelVbox.PackStart(detailsExpander, true, true, 0);

            var scrolledWindow = ScrolledWindow.New();
            var textView = TextView.New();
            scrolledWindow.Add(textView);

            scrolledWindow.SetMinContentWidth(650);
            scrolledWindow.SetMinContentHeight(250);
            
            textView.Editable = false;
            textView.Buffer.Text = debugInfo;

            detailsExpander.Add(scrolledWindow);
            
            ShowAll();

            AddButton("Close", ResponseType.Close);
        }

        private string BuildExceptionMessage(Exception e)
        {
            System.Text.StringBuilder msg = new System.Text.StringBuilder();

            msg.Append(Catalog.GetString("An unhandled exception was thrown: "));

            Stack<Exception> exception_chain = new Stack<Exception> ();

            while (e != null) {
                exception_chain.Push (e);
                e = e.InnerException;
            }

            while (exception_chain.Count > 0) {
                e = exception_chain.Pop ();
                msg.AppendFormat ("{0}\n\n{1}\n", e.Message, e.StackTrace);
            };

            msg.Append("\n");
            msg.AppendFormat(".NET Version: {0}\n", Environment.Version);
            msg.AppendFormat("OS Version: {0}\n", Environment.OSVersion);
            msg.Append("\nAssembly Version Information:\n\n");

            foreach(Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) {
                AssemblyName name = asm.GetName();
                msg.AppendFormat("{0} ({1})\n", name.Name, name.Version);
            }

            // if (Environment.OSVersion.Platform != PlatformID.Unix) {
            //     return msg.ToString();
            // }

            // TODO: Print helpful information relating to the user's
            // distribution if we are on Linux.

            return msg.ToString();
        }
    }
}
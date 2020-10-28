using Gtk;
using System;
using Hyena.Gui.Dialogs;

namespace Hyena.Gui.Test
{
    static class Program
    {
        static void Main()
        {
            Gtk.Global.Init();

            // Warmup the typedict
            // TODO: Static registration
            var box = new Box(Orientation.vertical);
            box.Dispose();
            var buf = new TextBuffer();
            buf.Dispose();
            var style = new StyleContext();
            style.Dispose();

            // TODO: Avoid the need for this
            Paths.ApplicationName = "HyenaTest";

            var dlg = new ExceptionDialog(new NotImplementedException());
            dlg.OnDestroy += (o, e) => Gtk.Global.MainQuit();
            dlg.ShowAll();


            Gtk.Global.Main();
        }
    }
}
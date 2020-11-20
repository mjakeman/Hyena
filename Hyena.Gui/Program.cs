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

            // TODO: Avoid the need for this
            Paths.ApplicationName = "HyenaTest";

            var dlg = new ExceptionDialog(new NotImplementedException());
            dlg.OnDestroy += (o, e) => Gtk.Global.MainQuit();
            dlg.ShowAll();


            Gtk.Global.Main();
        }
    }
}
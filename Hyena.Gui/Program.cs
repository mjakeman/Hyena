using Gtk;
using System;
using Hyena.Gui.Dialogs;

namespace Hyena.Gui.Test
{
    static class Program
    {
        static void Main()
        {
            Gtk.Functions.Init();

            // TODO: Avoid the need for this
            Paths.ApplicationName = "HyenaTest";

            var dlg = new ExceptionDialog(new NotImplementedException());
            dlg.OnResponse += (_, _) => Gtk.Functions.MainQuit();
            dlg.ShowAll();


            Gtk.Functions.Main();
        }
    }
}
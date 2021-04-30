using Gtk;
using System;
using Hyena.Gui.Dialogs;

namespace Hyena.Gui.Test
{
    static class Program
    {
        static void Main()
        {
            Functions.Init();

            // TODO: Avoid the need for this
            Paths.ApplicationName = "HyenaTest";

            var dlg = new ExceptionDialog(new NotImplementedException());
            dlg.OnDestroy += (o, e) => Functions.MainQuit();
            dlg.ShowAll();


            Functions.Main();
        }
    }
}
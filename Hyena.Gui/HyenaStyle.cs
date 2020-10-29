using System;
using System.IO;
using System.Text;
using System.Reflection;

using Gtk;

namespace Hyena.Gui
{
    public static class HyenaStyleExtensions
    {
        public static void AddHyenaStyleClass(this Widget widget, string str)
        {
            HyenaStyle.EnsureStylesheet();
            var styleContext = widget.GetStyleContext();
            styleContext.AddClass(str);
        }

        public static void RemoveHyenaStyleClass(this Widget widget, string str)
        {
            HyenaStyle.EnsureStylesheet();
            var styleContext = widget.GetStyleContext();
            styleContext.RemoveClass(str);
        }
    }

    internal static class HyenaStyle
    {
        private static CssProvider provider = null;
        private static bool usingStylesheet = false;
        private const uint APPLICATION_PRIORITY = 600;

        public static CssProvider GlobalCssProvider
        {
            get {
                if (provider == null)
                {
                    var asm = Assembly.GetAssembly(typeof(HyenaStyle));
                    provider = CreateProvider(asm);
                }

                return provider;
            }
        }

        public static void EnsureStylesheet()
        {
            if (!usingStylesheet)
            {
                var screen = Gdk.Screen.GetDefault();
                StyleContext.AddProviderForScreen(screen, GlobalCssProvider, 600);
            }
        }

        private static CssProvider CreateProvider(Assembly asm)
        {
            var provider = new CssProvider();

            // Debug Resource Names:
            // foreach (string name in asm.GetManifestResourceNames())
                // Console.WriteLine(name);

            using (var stream = asm.GetManifestResourceStream("Hyena.Gui.style.css"))
            {
                if (stream == null)
                    throw new Exception("Could not load stylesheet 'Hyena.Gui.style.css'");
                var cssStr = ReadFromStream(stream);
                provider.LoadFromData(cssStr, out var error);

                if (error != null)
                    throw new Exception(error?.Message);
            }

            return provider;
        }

        private static string ReadFromStream(Stream stream)
        {
            byte[] buffer;
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                buffer = ms.ToArray();
            }     

            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }
    }
}
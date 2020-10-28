using System;

namespace DBus
{
    public class InterfaceAttribute : Attribute
    {
        // Does absolutely nothing
        public InterfaceAttribute(string text)
        {
            Console.WriteLine("DBus Interface Attribute used - Not Implemented (See Hyena.Glue/DBus.cs)");
        }
    }
}
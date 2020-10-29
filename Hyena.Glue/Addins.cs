// using System;
// using System.Runtime.InteropServices;
// using System.Collections.Generic;

// namespace Mono.Addins
// {
//     // Stub code
//     // I don't actually know how Mono.Addins works
//     static public class AddinManager
//     {
//         public static bool IsInitialized => false;

//         public static List<TypeExtensionNode> GetExtensionNodes(string exPoint)
//         {
//             Console.WriteLine($"Extension Point Accessed: {exPoint} - Not Implemented");
//             return new List<TypeExtensionNode>();
//         }

//         public static void AddExtensionNodeHandler(string node, ExtensionNodeHandler handler)
//         {
//             Console.WriteLine($"Extension Node Handler Added: {node} - Not Implemented");
//         }
//     }

//     public delegate void ExtensionNodeHandler(object o, ExtensionNodeEventArgs e);

//     public class ExtensionNodeEventArgs : EventArgs
//     {
//         public object ExtensionNode => throw new NotImplementedException("ExtensionNode - NotImplemented");
//         public string Path => String.Empty;
//     }

//     public class TypeExtensionNode
//     {
//         public bool HasId => false;
//         public int Id => 0;

//         public object CreateInstance() => throw new NotImplementedException("Mono.Addins is not implemented! See Hyena.Glue/Addins.cs");
//         public object CreateInstance(Type t) => CreateInstance();
//     }
// }
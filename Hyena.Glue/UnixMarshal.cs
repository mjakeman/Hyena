using System;
using System.Runtime.InteropServices;

namespace Mono
{
    namespace Unix
    {
        public class UnixMarshal
        {
            // firox263 27/10/2020: Thanks Mono!
            // Glue code that implements PtrToStringArray for compatibility. We
            // want to move from Mono to .NET Core standard APIs as soon as possible,
            // so all methods are marked Obselete.

            [Obsolete("Use .NET APIs directly")]
            public static string[] PtrToStringArray (IntPtr stringArray)
            {
                if (stringArray == IntPtr.Zero)
                    return new string[]{};

                int argc = CountStrings (stringArray);
                return PtrToStringArray (argc, stringArray);
            }

            [Obsolete("Use .NET APIs directly")]
            private static int CountStrings (IntPtr stringArray)
            {
                int count = 0;
                while (Marshal.ReadIntPtr (stringArray, count*IntPtr.Size) != IntPtr.Zero)
                    ++count;
                return count;
            }

            /*
            * Like PtrToStringArray(IntPtr), but it allows the user to specify how
            * many strings to look for in the array.  As such, the requirement for a
            * terminating NULL element is not required.
            *
            * Usage is similar to ANSI C `main': count is argc, stringArray is argv.
            * stringArray[count] is NOT accessed (though ANSI C requires that 
            * argv[argc] = NULL, which PtrToStringArray(IntPtr) requires).
            */
            [Obsolete("Use .NET APIs directly")]
            public static string[] PtrToStringArray (int count, IntPtr stringArray)
            {
                if (count < 0)
                    throw new ArgumentOutOfRangeException ("count", "< 0");
                if (stringArray == IntPtr.Zero)
                    return new string[count];

                string[] members = new string[count];
                for (int i = 0; i < count; ++i) {
                    IntPtr s = Marshal.ReadIntPtr (stringArray, i * IntPtr.Size);
                    members[i] = Marshal.PtrToStringAnsi (s);
                }

                return members;
            }
        }
    }
}
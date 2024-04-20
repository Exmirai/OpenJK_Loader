using OpenJKLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Interop
{
    public static class OpenJK
    {
        public static class Linux
        {
            [DllImport("/app/openjkruntime/mbiided.i386.so", EntryPoint = "main", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern int EntryPoint(int argc, [In][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] argv);

            [DllImport("/app/openjkruntime/mbiided.i386.so", EntryPoint = "SV_BindLoader", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr BindLoader(ref LoaderImport imports);
        }

        public static class Windows
        {
            [DllImport("mbiided.x86.dll", EntryPoint = "main", CharSet = CharSet.Ansi)]
            public static extern int EntryPoint(int argc, [In][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] argv);

            [DllImport("mbiided.x86.dll", EntryPoint = "SV_BindLoader", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr BindLoader(ref LoaderImport imports);

        }


        public static void EntryPoint(IEnumerable<string> commandLineArguments)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Linux.EntryPoint(commandLineArguments.Count(), commandLineArguments.ToArray());
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Windows.EntryPoint(commandLineArguments.Count(), commandLineArguments.ToArray());
                return;
            }
        }

        public static IntPtr BindLoader(ref LoaderImport imports)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Linux.BindLoader(ref imports);
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Windows.BindLoader(ref imports);
            }
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Interop
{
    public static class Kernel
    {
        public static class Windows
        {
            [DllImport("Kernel32.dll", SetLastError = true)]
            public static extern int SetStdHandle(int device, SafeHandle handle);

            [DllImport("msvcrt.dll", EntryPoint = "_open_osfhandle", SetLastError = true)]
            public static extern int _open_osfhandle(SafeHandle handle, int mode);

            [DllImport("msvcrt.dll", EntryPoint = "_dup2", SetLastError = true)]
            public static extern int _dup2(int fd1, int fd2);

            [DllImport("kernel32.dll")]
            public static extern IntPtr LoadLibrary(string dllToLoad);
        }

        public static class Linux
        {
            [DllImport("libc", SetLastError = true)]
            public static extern int dup2(int fd, int fd2);
            [DllImport("libdl", SetLastError = true)]
            public static extern IntPtr dlopen(string dllToLoad, int flags);
        }


        public static async Task SetOverrideStdErrToStdOut(ILogger logger)
        {
            // setup stdout pipe    
            var namedPipeServer = new AnonymousPipeServerStream(PipeDirection.In);
            var streamReader = new StreamReader(namedPipeServer);
            var namedPipClient = new AnonymousPipeClientStream(PipeDirection.Out, namedPipeServer.ClientSafePipeHandle);

            nint fd;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                fd = namedPipeServer.ClientSafePipeHandle.DangerousGetHandle();
                Linux.dup2((int)fd, 2);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                fd = Windows._open_osfhandle(namedPipClient.SafePipeHandle, 0x4000);
                Windows._dup2((int)fd, 2);
            }
            else
            {
                throw new NotImplementedException();
            }

            while (true)
            {
                var str = await streamReader.ReadLineAsync();

                logger.LogInformation(str);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Interceptor.Extensions
{
    public static class ProcessExtensions
    {
        [Flags]
        private enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags desiredAccess,
                                                  [MarshalAs(UnmanagedType.Bool)] bool inheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("ntdll.dll")]
        private static extern uint NtResumeProcess(IntPtr processHandle);

        [DllImport("ntdll.dll")]
        private static extern uint NtSuspendProcess(IntPtr processHandle);

        public static void Suspend(this Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            var processHandle = OpenProcess(ProcessAccessFlags.All, false, process.Id);

            if (processHandle == IntPtr.Zero)
                throw new ArgumentException("Process ID is invalid, cannot call OpenProcess() to obtain a handle.");

            NtSuspendProcess(processHandle);

            CloseHandle(processHandle);
        }

        public static void Resume(this Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            var processHandle = OpenProcess(ProcessAccessFlags.All, false, process.Id);

            if (processHandle == IntPtr.Zero)
                throw new ArgumentException("Process ID is invalid, cannot call OpenProcess() to obtain a handle.");

            NtResumeProcess(processHandle);

            CloseHandle(processHandle);
        }
    }
}

using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Ipc;
using System.Security.Principal;
using EasyHook;
using RemotingInterface;

namespace Injector
{
    public static class InjectionHelper
    {
        public static string IpcChannelName = String.Format("Starcraft 2 AI Bot - {0} {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
        private static IpcServerChannel ipcServer;

        public static void IpcServerCreateListeningChannel(IpcInterface ipcInterface)
        {
            ipcServer = RemoteHooking.IpcCreateServer<IpcInterface>(ref IpcChannelName, WellKnownObjectMode.Singleton, ipcInterface, IpcChannelName, true, WellKnownSidType.WorldSid);
        }

        /// <summary>
        /// Injects the 32-bit and (optional) 64-bit DLL into the target process.
        /// </summary>
        /// <param name="x86Dll">The filename or full path to the 32-bit DLL.</param>
        /// <param name="x64Dll">The filename or full path to the 32-bit DLL.</param>
        /// <param name="processId">The process ID to inject to.</param>
        public static void Inject(String x86Dll, String x64Dll, int processId)
        {
            RemoteHooking.Inject(processId, InjectionOptions.Default, x86Dll, x64Dll, IpcChannelName); 
        }
    }
}

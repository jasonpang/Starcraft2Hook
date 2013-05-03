using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using EasyHook;
using Interceptor.Extensions;
using Logging;
using NLog;
using RemotingInterface;

namespace Interceptor
{
    public sealed class EntryPoint : IEntryPoint
    {
        private Logger Log { get; set; }
        private IpcInterface IpcInterface { get; set; }
        private string IpcChannelName { get; set; }

        public EntryPoint(RemoteHooking.IContext context, string channelName)
        {
            try
            {
                LoggingManager.Initialize();
                Log = LogManager.GetCurrentClassLogger();
                Log.Trace("Entering EntryPoint ctor().");
                Log.Info("DLL injection suceeded.");

                Log.Trace ("Establishing IPC interface...");
                this.IpcChannelName = channelName;
                this.IpcInterface = RemoteHooking.IpcConnectClient<IpcInterface>(channelName);
                Log.Trace("Pinging remote IPC server...");
                this.IpcInterface.Ping();
                Log.Trace("Exiting EntryPoint ctor().");
            }
            catch (Exception ex)
            {
                Log.Fatal (ex);
            }
        }

        public void Run(RemoteHooking.IContext context, string channelName)
        {
            try
            {
                Log.Trace ("Entering EntryPoint Run().");

                if (IpcInterface.StartPaused)
                {
                    Process.GetCurrentProcess().Suspend();
                }

                Log.Trace ("Installing game hooks...");
                Game.EntryPoint.InstallGameHooks();

                // TODO: AI.EntryPoint.Run();
                while (true)
                {
                    Thread.Sleep (0);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal (ex);
            }
            finally
            {
                Log.Trace("Uninstalling game hooks...");
                Game.EntryPoint.UninstallGameHooks();
                Log.Trace ("Exiting EntryPoint Run().");
            }
        }
    }
}

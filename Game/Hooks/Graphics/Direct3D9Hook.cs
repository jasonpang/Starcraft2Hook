using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Logging.Extensions;
using NLog;

namespace Game.Hooks.Graphics
{
    public partial class Direct3D9Hook : HookBase
    {
        private readonly Logger Log = LogManager.GetCurrentClassLogger();

        public override void Install()
        {
            try
            {
                if (false)
#pragma warning disable 162
                {
                    MessageBox.Show (
                                     String.Format ("{0}.exe is now paused. Press OK to resume.",
                                                    Process.GetCurrentProcess()
                                                           .ProcessName), "Process Paused", MessageBoxButtons.OK,
                                     MessageBoxIcon.Information);
                }
#pragma warning restore 162

                Log.Trace ("Compiling Direct3D 9 virtual table function addresses...");
                this.CompileDirect3D9FunctionAddresses();

                Log.Trace ("Setting up Direct3D 9 hooks...");
                this.InstallDirect3D9Hooks();

                Log.Trace ("Activating Direct3D 9 hooks...");
                this.ActivateHooks();
            }
            catch (Exception ex)
            {
                this.Log.Fatal (ex);
            }
        }

        public void InstallAllExcept (params Direct3D9DeviceFunctions[] functions)
        {
            Log.Trace("Hooking all functions except: {0}", functions.PrintValues());
            this.ExcludedHooks.AddRange (functions);

            this.Install();
        }

        public void InstallOnly (params Direct3D9DeviceFunctions[] functions)
        {
            Log.Trace ("Only hooking functions: {0}", functions.PrintValues());
            var values = Enum.GetValues (typeof (Direct3D9DeviceFunctions))
                             .Cast <Direct3D9DeviceFunctions>()
                             .ToList();

            foreach (var function in functions)
                values.Remove (function);

            this.InstallAllExcept (values.ToArray());
        }

        public override void Uninstall()
        {
            foreach (var hook in Hooks)
            {
                var localHook = hook.Value;
                localHook.Dispose();
            }
        }
    }
}

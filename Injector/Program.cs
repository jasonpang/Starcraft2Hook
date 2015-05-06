using System;
using System.Windows.Forms;
using Cinchoo.Core;
using Injector.Extensions;
using Injector.Properties;

namespace Injector
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            //SetupApplicationConfiguration();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        private static void SetupApplicationConfiguration()
        {
            ChoApplication.ApplyFrxParamsOverrides += new EventHandler<ChoFrxParamsEventArgs>(ChoApplication_ApplyFrxParamsOverrides);
            ChoFramework.Initialize();
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => ChoFramework.Shutdown();
        }

        private static void ChoApplication_ApplyFrxParamsOverrides(object sender, ChoFrxParamsEventArgs e)
        {
            e.GlobalApplicationSettings.ApplicationBehaviourSettings.ActivateFirstInstance = true;
            e.GlobalApplicationSettings.ApplicationBehaviourSettings.SingleInstanceApp = true;
            e.GlobalApplicationSettings.LogSettings.LogFolder = @"Config\Logs";
            e.GlobalApplicationSettings.LogSettings.LogFileName = "cholog.log";
            e.GlobalApplicationSettings.TrayApplicationBehaviourSettings.HideTrayIconWhenMainWindowShown = true;
            e.GlobalApplicationSettings.TrayApplicationBehaviourSettings.ShowInTaskbar = true;
            e.GlobalApplicationSettings.TrayApplicationBehaviourSettings.TooltipText = "Starcraft 2 A.I. Bot";
            e.GlobalApplicationSettings.TrayApplicationBehaviourSettings.TrayAppTurnOnMode = ChoTrayAppTurnOnMode.OnMinimize;
            e.GlobalApplicationSettings.TrayApplicationBehaviourSettings.TrayIcon = Resources.Starcraft2Icon.GetIconAsSavedFilePath();
        }
    }
}

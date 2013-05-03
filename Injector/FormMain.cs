using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Injector.Configuration;
using Injector.Extensions;
using RemotingInterface;

namespace Injector
{
    public partial class FormMain : Form
    {
        private IpcInterface IpcInterface { get; set; }

        public FormMain()
        {
            this.InitializeComponent();
        }

        private void FormInjector_Load(object sender, System.EventArgs e)
        {
            if (Config.Window.SaveWindowPositionOnExit)
                this.Location = Config.Window.LastWindowPosition;

            if (Config.Injection.StartProcessPaused)
                this.CheckStartPaused.Checked = Config.Injection.StartProcessPaused;

            if (Config.Injection.AutoInject)
                this.CheckBoxAutoInject.Checked = Config.Injection.AutoInject;
        }

        private void FormInjector_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Config.Window.SaveWindowPositionOnExit)
                Config.Window.LastWindowPosition = this.Location;

            Config.Injection.StartProcessPaused = this.CheckStartPaused.Checked;

            Config.Injection.AutoInject = this.CheckBoxAutoInject.Checked;
        }

        private void ButtonInject_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.ButtonInject.IsDepressed)
            {
                try
                {
                    var processName = this.TextBoxProcess.Text;
                    var processes = Process.GetProcessesByName(processName);

                    if (processes.Any())
                    {
                        this.IpcInterface = new IpcInterface();

                        if (CheckStartPaused.Checked)
                            IpcInterface.StartPaused = true;

                        InjectionHelper.IpcServerCreateListeningChannel(this.IpcInterface);

                        EasyHook.Config.Register("Starcraft 2 AI Bot",
                                                 "Logging.dll",
                                                 "RemotingInterface.dll",
                                                 "Interceptor.dll",
                                                 "Game.dll",
                                                 "Direct3D9Hook.dll",
                                                 "NLog.dll",
                                                 "SharpDX.dll",
                                                 "SharpDX.Direct3D9.dll");

                        InjectionHelper.Inject("Interceptor.dll", "Interceptor.dll", processes.First().Id);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Injection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Should exit, but not implemented
            }
        }

        private void ButtonUpdate1_Click(object sender, EventArgs e)
        {
            try
            {
               // IpcInterface.WallhackTextureNumVerticesThreshold = (int) (TextBoxMinVertices.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.TextBoxMinVertices.Value = 0;
            }
        }

        private void CheckBoxAutoInject_CheckedChanged(object sender, EventArgs e)
        {
            this.TimerAutoInject.Enabled = this.CheckBoxAutoInject.Checked;
        }

        private void TimerAutoInject_Tick(object sender, EventArgs e)
        {
            this.TimerAutoInject.Enabled = false;

            try
            {
                var processName = this.TextBoxProcess.Text;
                var processes = Process.GetProcessesByName(processName);

                if (processes.Any())
                {
                    this.CheckBoxAutoInject.Checked = false;
                    this.TimerAutoInject.Enabled = false;
                    this.ButtonInject.Checked = true;
                }
                else
                {
                    this.TimerAutoInject.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                this.CheckBoxAutoInject.Checked = false;
                this.TimerAutoInject.Enabled = false;
                MessageBox.Show(ex.ToString(), "Auto Injection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonUpdateTextureStage_Click(object sender, EventArgs e)
        {
            try
            {
                //IpcInterface.TextureStage = (int) (TextBoxTextureStage.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.TextBoxMinVertices.Value = 0;
            }
        }
    }
}

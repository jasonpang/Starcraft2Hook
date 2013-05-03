using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Injector.Controls
{
    public partial class ToggleButton : CheckBox
    {
        [Description("Displayed when the button is depressed.")]
        public String ToggleTextDepressed { get; set; }

        [Description("Displayed when the button is raised.")]
        public String ToggleTextRaised { get; set; }

        public ToggleButton()
        {
            this.InitializeComponent();

            // Set the appearance to be that of a button
            this.Appearance = Appearance.Button;
            this.TextAlign = ContentAlignment.MiddleCenter;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void ToggleButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Checked)
            {
                this.Text = this.ToggleTextDepressed;
            }
            else
            {
                this.Text = this.ToggleTextRaised;
            }
        }

        public bool IsDepressed
        {
            get { return this.Checked; }
        }
    }
}

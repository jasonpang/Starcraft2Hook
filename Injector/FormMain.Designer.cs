using Injector.Controls;

namespace Injector
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.CheckBoxAutoInject = new System.Windows.Forms.CheckBox();
            this.CheckStartPaused = new System.Windows.Forms.CheckBox();
            this.ButtonInject = new Injector.Controls.ToggleButton();
            this.TextBoxProcess = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Log = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextBoxTextureStage = new System.Windows.Forms.NumericUpDown();
            this.TextBoxMinVertices = new System.Windows.Forms.NumericUpDown();
            this.ButtonUpdateTextureStage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ButtonUpdate1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TimerAutoInject = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TextBoxTextureStage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBoxMinVertices)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(573, 273);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.CheckBoxAutoInject);
            this.tabPage1.Controls.Add(this.CheckStartPaused);
            this.tabPage1.Controls.Add(this.ButtonInject);
            this.tabPage1.Controls.Add(this.TextBoxProcess);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(565, 243);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Injection";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAutoInject
            // 
            this.CheckBoxAutoInject.AutoSize = true;
            this.CheckBoxAutoInject.Location = new System.Drawing.Point(4, 37);
            this.CheckBoxAutoInject.Name = "CheckBoxAutoInject";
            this.CheckBoxAutoInject.Size = new System.Drawing.Size(138, 21);
            this.CheckBoxAutoInject.TabIndex = 5;
            this.CheckBoxAutoInject.Text = "Automatically inject";
            this.CheckBoxAutoInject.UseVisualStyleBackColor = true;
            this.CheckBoxAutoInject.CheckedChanged += new System.EventHandler(this.CheckBoxAutoInject_CheckedChanged);
            // 
            // CheckStartPaused
            // 
            this.CheckStartPaused.AutoSize = true;
            this.CheckStartPaused.Location = new System.Drawing.Point(468, 37);
            this.CheckStartPaused.Name = "CheckStartPaused";
            this.CheckStartPaused.Size = new System.Drawing.Size(100, 21);
            this.CheckStartPaused.TabIndex = 4;
            this.CheckStartPaused.Text = "Start Paused";
            this.CheckStartPaused.UseVisualStyleBackColor = true;
            // 
            // ButtonInject
            // 
            this.ButtonInject.Appearance = System.Windows.Forms.Appearance.Button;
            this.ButtonInject.Location = new System.Drawing.Point(468, 4);
            this.ButtonInject.Name = "ButtonInject";
            this.ButtonInject.Size = new System.Drawing.Size(94, 27);
            this.ButtonInject.TabIndex = 3;
            this.ButtonInject.Text = "Inject";
            this.ButtonInject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ButtonInject.ToggleTextDepressed = "Injected";
            this.ButtonInject.ToggleTextRaised = "Inject";
            this.ButtonInject.UseVisualStyleBackColor = true;
            this.ButtonInject.CheckedChanged += new System.EventHandler(this.ButtonInject_CheckedChanged);
            // 
            // TextBoxProcess
            // 
            this.TextBoxProcess.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.TextBoxProcess.Location = new System.Drawing.Point(188, 6);
            this.TextBoxProcess.Name = "TextBoxProcess";
            this.TextBoxProcess.Size = new System.Drawing.Size(105, 25);
            this.TextBoxProcess.TabIndex = 2;
            this.TextBoxProcess.Text = "SC2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Process Name (no extension):";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Log);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(565, 243);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Messages";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Log
            // 
            this.Log.BackColor = System.Drawing.Color.White;
            this.Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Log.Location = new System.Drawing.Point(3, 3);
            this.Log.Multiline = true;
            this.Log.Name = "Log";
            this.Log.ReadOnly = true;
            this.Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Log.Size = new System.Drawing.Size(559, 237);
            this.Log.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tabControl2);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(565, 243);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Control";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(559, 237);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(551, 207);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "DrawIndexedPrimitive";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBoxTextureStage);
            this.groupBox1.Controls.Add(this.TextBoxMinVertices);
            this.groupBox1.Controls.Add(this.ButtonUpdateTextureStage);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ButtonUpdate1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Maphack";
            // 
            // TextBoxTextureStage
            // 
            this.TextBoxTextureStage.Location = new System.Drawing.Point(9, 104);
            this.TextBoxTextureStage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.TextBoxTextureStage.Name = "TextBoxTextureStage";
            this.TextBoxTextureStage.Size = new System.Drawing.Size(100, 25);
            this.TextBoxTextureStage.TabIndex = 1;
            this.TextBoxTextureStage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextBoxMinVertices
            // 
            this.TextBoxMinVertices.Location = new System.Drawing.Point(9, 49);
            this.TextBoxMinVertices.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.TextBoxMinVertices.Name = "TextBoxMinVertices";
            this.TextBoxMinVertices.Size = new System.Drawing.Size(100, 25);
            this.TextBoxMinVertices.TabIndex = 1;
            this.TextBoxMinVertices.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ButtonUpdateTextureStage
            // 
            this.ButtonUpdateTextureStage.Location = new System.Drawing.Point(115, 107);
            this.ButtonUpdateTextureStage.Name = "ButtonUpdateTextureStage";
            this.ButtonUpdateTextureStage.Size = new System.Drawing.Size(75, 25);
            this.ButtonUpdateTextureStage.TabIndex = 2;
            this.ButtonUpdateTextureStage.Text = "Update";
            this.ButtonUpdateTextureStage.UseVisualStyleBackColor = true;
            this.ButtonUpdateTextureStage.Click += new System.EventHandler(this.ButtonUpdateTextureStage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Texture Stage";
            // 
            // ButtonUpdate1
            // 
            this.ButtonUpdate1.Location = new System.Drawing.Point(115, 49);
            this.ButtonUpdate1.Name = "ButtonUpdate1";
            this.ButtonUpdate1.Size = new System.Drawing.Size(75, 25);
            this.ButtonUpdate1.TabIndex = 1;
            this.ButtonUpdate1.Text = "Update";
            this.ButtonUpdate1.UseVisualStyleBackColor = true;
            this.ButtonUpdate1.Click += new System.EventHandler(this.ButtonUpdate1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Minimum Vertices to disable Z-Buffering:";
            // 
            // TimerAutoInject
            // 
            this.TimerAutoInject.Interval = 15;
            this.TimerAutoInject.Tick += new System.EventHandler(this.TimerAutoInject_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(573, 273);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(589, 39);
            this.Name = "FormMain";
            this.Text = "Starcraft 2 A.I. Injector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormInjector_FormClosing);
            this.Load += new System.EventHandler(this.FormInjector_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TextBoxTextureStage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBoxMinVertices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox TextBoxProcess;
        private System.Windows.Forms.Label label1;
        private Controls.ToggleButton ButtonInject;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox Log;
        private System.Windows.Forms.CheckBox CheckStartPaused;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ButtonUpdate1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CheckBoxAutoInject;
        private System.Windows.Forms.Timer TimerAutoInject;
        private System.Windows.Forms.Button ButtonUpdateTextureStage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown TextBoxTextureStage;
        private System.Windows.Forms.NumericUpDown TextBoxMinVertices;
    }
}


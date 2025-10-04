namespace BigRedButton
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            radioKeypress = new RadioButton();
            radioPowerShell = new RadioButton();
            textBoxKeypress = new TextBox();
            textBoxPowerShell = new TextBox();
            buttonBrowse = new Button();
            buttonRecord = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            checkBoxLaunchOnStartup = new CheckBox();
            checkBoxStartMinimised = new CheckBox();
            notifyIcon = new NotifyIcon(components);
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // radioKeypress
            // 
            radioKeypress.Checked = true;
            radioKeypress.Dock = DockStyle.Fill;
            radioKeypress.Location = new Point(13, 13);
            radioKeypress.Name = "radioKeypress";
            radioKeypress.Size = new Size(140, 23);
            radioKeypress.TabIndex = 0;
            radioKeypress.TabStop = true;
            radioKeypress.Text = "Keypress";
            // 
            // radioPowerShell
            // 
            radioPowerShell.Dock = DockStyle.Fill;
            radioPowerShell.Location = new Point(13, 42);
            radioPowerShell.Name = "radioPowerShell";
            radioPowerShell.Size = new Size(140, 23);
            radioPowerShell.TabIndex = 3;
            radioPowerShell.Text = "PowerShell Script";
            // 
            // textBoxKeypress
            // 
            textBoxKeypress.Dock = DockStyle.Fill;
            textBoxKeypress.Location = new Point(159, 13);
            textBoxKeypress.Name = "textBoxKeypress";
            textBoxKeypress.PlaceholderText = "Shortcut";
            textBoxKeypress.ReadOnly = true;
            textBoxKeypress.Size = new Size(470, 23);
            textBoxKeypress.TabIndex = 1;
            // 
            // textBoxPowerShell
            // 
            textBoxPowerShell.Dock = DockStyle.Fill;
            textBoxPowerShell.Location = new Point(159, 42);
            textBoxPowerShell.Name = "textBoxPowerShell";
            textBoxPowerShell.PlaceholderText = "Script path";
            textBoxPowerShell.Size = new Size(470, 23);
            textBoxPowerShell.TabIndex = 4;
            // 
            // buttonBrowse
            // 
            buttonBrowse.Dock = DockStyle.Fill;
            buttonBrowse.Location = new Point(635, 42);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new Size(150, 23);
            buttonBrowse.TabIndex = 5;
            buttonBrowse.Text = "Browse...";
            buttonBrowse.Click += OnButtonBrowseClick;
            // 
            // buttonRecord
            // 
            buttonRecord.Dock = DockStyle.Fill;
            buttonRecord.Location = new Point(635, 13);
            buttonRecord.Name = "buttonRecord";
            buttonRecord.Size = new Size(150, 23);
            buttonRecord.TabIndex = 2;
            buttonRecord.Text = "Record";
            buttonRecord.Click += OnButtonRecordClick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            tableLayoutPanel1.Controls.Add(buttonBrowse, 3, 2);
            tableLayoutPanel1.Controls.Add(textBoxPowerShell, 2, 2);
            tableLayoutPanel1.Controls.Add(textBoxKeypress, 2, 1);
            tableLayoutPanel1.Controls.Add(buttonRecord, 3, 1);
            tableLayoutPanel1.Controls.Add(radioKeypress, 1, 1);
            tableLayoutPanel1.Controls.Add(radioPowerShell, 1, 2);
            tableLayoutPanel1.Controls.Add(checkBoxLaunchOnStartup, 1, 3);
            tableLayoutPanel1.Controls.Add(checkBoxStartMinimised, 1, 4);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tableLayoutPanel1.Size = new Size(798, 136);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // checkBoxLaunchOnStartup
            // 
            tableLayoutPanel1.SetColumnSpan(checkBoxLaunchOnStartup, 3);
            checkBoxLaunchOnStartup.Dock = DockStyle.Fill;
            checkBoxLaunchOnStartup.Location = new Point(13, 71);
            checkBoxLaunchOnStartup.Name = "checkBoxLaunchOnStartup";
            checkBoxLaunchOnStartup.Size = new Size(772, 23);
            checkBoxLaunchOnStartup.TabIndex = 6;
            checkBoxLaunchOnStartup.Text = "Launch when computer starts";
            checkBoxLaunchOnStartup.CheckedChanged += OnLaunchOnStartupCheckedChanged;
            // 
            // checkBoxStartMinimised
            // 
            tableLayoutPanel1.SetColumnSpan(checkBoxStartMinimised, 3);
            checkBoxStartMinimised.Dock = DockStyle.Fill;
            checkBoxStartMinimised.Location = new Point(13, 100);
            checkBoxStartMinimised.Name = "checkBoxStartMinimised";
            checkBoxStartMinimised.Size = new Size(772, 23);
            checkBoxStartMinimised.TabIndex = 7;
            checkBoxStartMinimised.Text = "Start app minimised";
            // 
            // notifyIcon
            // 
            notifyIcon.BalloonTipText = "Not A Virus";
            notifyIcon.BalloonTipTitle = "Big Red Button";
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "Big Red Button";
            notifyIcon.Visible = true;
            notifyIcon.MouseClick += OnMouseClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(855, 179);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Big Red Button";
            FormClosing += OnFormClosing;
            Resize += MainForm_Resize;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.RadioButton radioKeypress;
        private System.Windows.Forms.RadioButton radioPowerShell;
        private System.Windows.Forms.TextBox textBoxKeypress;
        private System.Windows.Forms.TextBox textBoxPowerShell;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonRecord;
        private TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBoxLaunchOnStartup;
        private System.Windows.Forms.CheckBox checkBoxStartMinimised;
        private NotifyIcon notifyIcon;
    }
}

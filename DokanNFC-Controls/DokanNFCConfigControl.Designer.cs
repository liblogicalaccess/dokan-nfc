namespace DokanNFC
{
    partial class DokanNFCConfigControl
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gpReader = new System.Windows.Forms.GroupBox();
            this.cbMountPoint = new System.Windows.Forms.ComboBox();
            this.lblMountPoint = new System.Windows.Forms.Label();
            this.chkKeepMounted = new System.Windows.Forms.CheckBox();
            this.cbReaderUnit = new System.Windows.Forms.ComboBox();
            this.lblReaderUnit = new System.Windows.Forms.Label();
            this.cbReaderProvider = new System.Windows.Forms.ComboBox();
            this.lblReaderProvider = new System.Windows.Forms.Label();
            this.gpMode = new System.Windows.Forms.GroupBox();
            this.rbtnModeNFC = new System.Windows.Forms.RadioButton();
            this.rbtnModeRaw = new System.Windows.Forms.RadioButton();
            this.lblMode = new System.Windows.Forms.Label();
            this.gpReader.SuspendLayout();
            this.gpMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpReader
            // 
            this.gpReader.Controls.Add(this.cbMountPoint);
            this.gpReader.Controls.Add(this.lblMountPoint);
            this.gpReader.Controls.Add(this.chkKeepMounted);
            this.gpReader.Controls.Add(this.cbReaderUnit);
            this.gpReader.Controls.Add(this.lblReaderUnit);
            this.gpReader.Controls.Add(this.cbReaderProvider);
            this.gpReader.Controls.Add(this.lblReaderProvider);
            this.gpReader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpReader.Location = new System.Drawing.Point(0, 82);
            this.gpReader.Name = "gpReader";
            this.gpReader.Size = new System.Drawing.Size(405, 118);
            this.gpReader.TabIndex = 4;
            this.gpReader.TabStop = false;
            this.gpReader.Text = "Reader";
            // 
            // cbMountPoint
            // 
            this.cbMountPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMountPoint.FormattingEnabled = true;
            this.cbMountPoint.Location = new System.Drawing.Point(105, 69);
            this.cbMountPoint.Name = "cbMountPoint";
            this.cbMountPoint.Size = new System.Drawing.Size(44, 21);
            this.cbMountPoint.TabIndex = 6;
            // 
            // lblMountPoint
            // 
            this.lblMountPoint.AutoSize = true;
            this.lblMountPoint.Location = new System.Drawing.Point(33, 72);
            this.lblMountPoint.Name = "lblMountPoint";
            this.lblMountPoint.Size = new System.Drawing.Size(66, 13);
            this.lblMountPoint.TabIndex = 5;
            this.lblMountPoint.Text = "Mount point:";
            // 
            // chkKeepMounted
            // 
            this.chkKeepMounted.AutoSize = true;
            this.chkKeepMounted.Location = new System.Drawing.Point(105, 96);
            this.chkKeepMounted.Name = "chkKeepMounted";
            this.chkKeepMounted.Size = new System.Drawing.Size(273, 17);
            this.chkKeepMounted.TabIndex = 4;
            this.chkKeepMounted.Text = "Keep the drive mounted even whithout card inserted";
            this.chkKeepMounted.UseVisualStyleBackColor = true;
            // 
            // cbReaderUnit
            // 
            this.cbReaderUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReaderUnit.FormattingEnabled = true;
            this.cbReaderUnit.Location = new System.Drawing.Point(105, 42);
            this.cbReaderUnit.Name = "cbReaderUnit";
            this.cbReaderUnit.Size = new System.Drawing.Size(174, 21);
            this.cbReaderUnit.TabIndex = 3;
            // 
            // lblReaderUnit
            // 
            this.lblReaderUnit.AutoSize = true;
            this.lblReaderUnit.Location = new System.Drawing.Point(32, 45);
            this.lblReaderUnit.Name = "lblReaderUnit";
            this.lblReaderUnit.Size = new System.Drawing.Size(67, 13);
            this.lblReaderUnit.TabIndex = 2;
            this.lblReaderUnit.Text = "Reader Unit:";
            // 
            // cbReaderProvider
            // 
            this.cbReaderProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReaderProvider.FormattingEnabled = true;
            this.cbReaderProvider.Location = new System.Drawing.Point(105, 15);
            this.cbReaderProvider.Name = "cbReaderProvider";
            this.cbReaderProvider.Size = new System.Drawing.Size(174, 21);
            this.cbReaderProvider.TabIndex = 1;
            this.cbReaderProvider.SelectedIndexChanged += new System.EventHandler(this.cbReaderProvider_SelectedIndexChanged);
            // 
            // lblReaderProvider
            // 
            this.lblReaderProvider.AutoSize = true;
            this.lblReaderProvider.Location = new System.Drawing.Point(12, 18);
            this.lblReaderProvider.Name = "lblReaderProvider";
            this.lblReaderProvider.Size = new System.Drawing.Size(87, 13);
            this.lblReaderProvider.TabIndex = 0;
            this.lblReaderProvider.Text = "Reader Provider:";
            // 
            // gpMode
            // 
            this.gpMode.Controls.Add(this.rbtnModeNFC);
            this.gpMode.Controls.Add(this.rbtnModeRaw);
            this.gpMode.Controls.Add(this.lblMode);
            this.gpMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpMode.Location = new System.Drawing.Point(0, 0);
            this.gpMode.Name = "gpMode";
            this.gpMode.Size = new System.Drawing.Size(405, 82);
            this.gpMode.TabIndex = 3;
            this.gpMode.TabStop = false;
            this.gpMode.Text = "Mode";
            // 
            // rbtnModeNFC
            // 
            this.rbtnModeNFC.AutoSize = true;
            this.rbtnModeNFC.Checked = true;
            this.rbtnModeNFC.Location = new System.Drawing.Point(36, 32);
            this.rbtnModeNFC.Name = "rbtnModeNFC";
            this.rbtnModeNFC.Size = new System.Drawing.Size(359, 17);
            this.rbtnModeNFC.TabIndex = 2;
            this.rbtnModeNFC.TabStop = true;
            this.rbtnModeNFC.Text = "NFC: chip is analyzed as an NFC Tag and approriate content displayed";
            this.rbtnModeNFC.UseVisualStyleBackColor = true;
            // 
            // rbtnModeRaw
            // 
            this.rbtnModeRaw.AutoSize = true;
            this.rbtnModeRaw.Location = new System.Drawing.Point(36, 55);
            this.rbtnModeRaw.Name = "rbtnModeRaw";
            this.rbtnModeRaw.Size = new System.Drawing.Size(224, 17);
            this.rbtnModeRaw.TabIndex = 1;
            this.rbtnModeRaw.Text = "Raw: chip content is displayed as raw files";
            this.rbtnModeRaw.UseVisualStyleBackColor = true;
            this.rbtnModeRaw.Visible = false;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(12, 16);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(389, 13);
            this.lblMode.TabIndex = 0;
            this.lblMode.Text = "Select the analyze and display mode for RFIC/NFC chips inserted on your reader.";
            // 
            // DokanNFCConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpReader);
            this.Controls.Add(this.gpMode);
            this.Name = "DokanNFCConfigControl";
            this.Size = new System.Drawing.Size(405, 200);
            this.Load += new System.EventHandler(this.DokanNFCConfigControl_Load);
            this.gpReader.ResumeLayout(false);
            this.gpReader.PerformLayout();
            this.gpMode.ResumeLayout(false);
            this.gpMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpReader;
        private System.Windows.Forms.ComboBox cbMountPoint;
        private System.Windows.Forms.Label lblMountPoint;
        private System.Windows.Forms.CheckBox chkKeepMounted;
        private System.Windows.Forms.ComboBox cbReaderUnit;
        private System.Windows.Forms.Label lblReaderUnit;
        private System.Windows.Forms.ComboBox cbReaderProvider;
        private System.Windows.Forms.Label lblReaderProvider;
        private System.Windows.Forms.GroupBox gpMode;
        private System.Windows.Forms.RadioButton rbtnModeNFC;
        private System.Windows.Forms.RadioButton rbtnModeRaw;
        private System.Windows.Forms.Label lblMode;
    }
}

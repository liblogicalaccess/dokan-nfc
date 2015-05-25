namespace DokanNFC
{
    partial class DokanNFCForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DokanNFCForm));
            this.gpMode = new System.Windows.Forms.GroupBox();
            this.rbtnModeNFC = new System.Windows.Forms.RadioButton();
            this.rbtnModeRaw = new System.Windows.Forms.RadioButton();
            this.lblMode = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gpReader = new System.Windows.Forms.GroupBox();
            this.cbMountPoint = new System.Windows.Forms.ComboBox();
            this.lblMountPoint = new System.Windows.Forms.Label();
            this.chkKeepMounted = new System.Windows.Forms.CheckBox();
            this.cbReaderUnit = new System.Windows.Forms.ComboBox();
            this.lblReaderUnit = new System.Windows.Forms.Label();
            this.cbReaderProvider = new System.Windows.Forms.ComboBox();
            this.lblReaderProvider = new System.Windows.Forms.Label();
            this.gpMode.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.gpReader.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpMode
            // 
            this.gpMode.Controls.Add(this.rbtnModeNFC);
            this.gpMode.Controls.Add(this.rbtnModeRaw);
            this.gpMode.Controls.Add(this.lblMode);
            this.gpMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpMode.Location = new System.Drawing.Point(0, 0);
            this.gpMode.Name = "gpMode";
            this.gpMode.Size = new System.Drawing.Size(403, 82);
            this.gpMode.TabIndex = 0;
            this.gpMode.TabStop = false;
            this.gpMode.Text = "Mode";
            // 
            // rbtnModeNFC
            // 
            this.rbtnModeNFC.AutoSize = true;
            this.rbtnModeNFC.Checked = true;
            this.rbtnModeNFC.Location = new System.Drawing.Point(35, 59);
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
            this.rbtnModeRaw.Enabled = false;
            this.rbtnModeRaw.Location = new System.Drawing.Point(35, 36);
            this.rbtnModeRaw.Name = "rbtnModeRaw";
            this.rbtnModeRaw.Size = new System.Drawing.Size(310, 17);
            this.rbtnModeRaw.TabIndex = 1;
            this.rbtnModeRaw.Text = "Raw: chip content is displayed as raw files (not available yet)";
            this.rbtnModeRaw.UseVisualStyleBackColor = true;
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
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Controls.Add(this.btnSave);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 197);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(403, 32);
            this.panelBottom.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(325, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.gpReader.Size = new System.Drawing.Size(403, 115);
            this.gpReader.TabIndex = 2;
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
            // DokanNFCForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(403, 229);
            this.Controls.Add(this.gpReader);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.gpMode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DokanNFCForm";
            this.Text = "DokanNFC Configuration";
            this.Load += new System.EventHandler(this.DokanNFCForm_Load);
            this.gpMode.ResumeLayout(false);
            this.gpMode.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.gpReader.ResumeLayout(false);
            this.gpReader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpMode;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.RadioButton rbtnModeNFC;
        private System.Windows.Forms.RadioButton rbtnModeRaw;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox gpReader;
        private System.Windows.Forms.ComboBox cbReaderProvider;
        private System.Windows.Forms.Label lblReaderProvider;
        private System.Windows.Forms.ComboBox cbReaderUnit;
        private System.Windows.Forms.Label lblReaderUnit;
        private System.Windows.Forms.CheckBox chkKeepMounted;
        private System.Windows.Forms.ComboBox cbMountPoint;
        private System.Windows.Forms.Label lblMountPoint;
    }
}


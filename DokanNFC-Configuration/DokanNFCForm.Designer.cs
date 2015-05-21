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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gpReader = new System.Windows.Forms.GroupBox();
            this.lblReaderProvider = new System.Windows.Forms.Label();
            this.cbReaderProvider = new System.Windows.Forms.ComboBox();
            this.cbReaderUnit = new System.Windows.Forms.ComboBox();
            this.lblReaderUnit = new System.Windows.Forms.Label();
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
            this.rbtnModeRaw.Location = new System.Drawing.Point(35, 36);
            this.rbtnModeRaw.Name = "rbtnModeRaw";
            this.rbtnModeRaw.Size = new System.Drawing.Size(224, 17);
            this.rbtnModeRaw.TabIndex = 1;
            this.rbtnModeRaw.Text = "Raw: chip content is displayed as raw files";
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
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnOk);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 153);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(403, 32);
            this.panelBottom.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.Location = new System.Drawing.Point(123, 6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(204, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gpReader
            // 
            this.gpReader.Controls.Add(this.cbReaderUnit);
            this.gpReader.Controls.Add(this.lblReaderUnit);
            this.gpReader.Controls.Add(this.cbReaderProvider);
            this.gpReader.Controls.Add(this.lblReaderProvider);
            this.gpReader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpReader.Location = new System.Drawing.Point(0, 82);
            this.gpReader.Name = "gpReader";
            this.gpReader.Size = new System.Drawing.Size(403, 71);
            this.gpReader.TabIndex = 2;
            this.gpReader.TabStop = false;
            this.gpReader.Text = "Reader";
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
            // DokanNFCForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(403, 185);
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
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gpReader;
        private System.Windows.Forms.ComboBox cbReaderProvider;
        private System.Windows.Forms.Label lblReaderProvider;
        private System.Windows.Forms.ComboBox cbReaderUnit;
        private System.Windows.Forms.Label lblReaderUnit;
    }
}


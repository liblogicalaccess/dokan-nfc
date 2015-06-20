namespace DokanNFC
{
    partial class DokanNFCSheet
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
            this.dokanNFCConfigControl = new DokanNFC.DokanNFCConfigControl();
            this.SuspendLayout();
            // 
            // dokanNFCConfigControl
            // 
            this.dokanNFCConfigControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dokanNFCConfigControl.Location = new System.Drawing.Point(0, 0);
            this.dokanNFCConfigControl.Name = "dokanNFCConfigControl";
            this.dokanNFCConfigControl.Size = new System.Drawing.Size(405, 200);
            this.dokanNFCConfigControl.TabIndex = 0;
            // 
            // DokanNFCSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dokanNFCConfigControl);
            this.Name = "DokanNFCSheet";
            this.Size = new System.Drawing.Size(405, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private DokanNFCConfigControl dokanNFCConfigControl;
    }
}

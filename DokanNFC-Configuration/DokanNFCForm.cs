using LibLogicalAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DokanNFC
{
    public partial class DokanNFCForm : Form
    {
        public DokanNFCForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DokanNFCConfig config = GetConfiguration();
                config.SaveToFile();
                MessageBox.Show(Properties.Resources.ConfigSaved, Properties.Resources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        public void SetConfiguration(DokanNFCConfig config)
        {
            dokanNFCConfigControl.SetConfiguration(config);
        }

        public DokanNFCConfig GetConfiguration()
        {
            return dokanNFCConfigControl.GetConfiguration();
        }
    }
}

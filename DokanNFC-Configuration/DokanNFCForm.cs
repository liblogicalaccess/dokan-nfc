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

            readerProviders = new Dictionary<string, IReaderProvider>();
            readerUnits = new Dictionary<string, IReaderUnit>();
        }

        Dictionary<string, IReaderProvider> readerProviders;
        Dictionary<string, IReaderUnit> readerUnits;

        private void DokanNFCForm_Load(object sender, EventArgs e)
        {
            InitializeReaderProviderList();
        }

        private void InitializeReaderProviderList()
        {
            readerProviders.Clear();
            cbReaderProvider.Items.Clear();

            readerProviders.Add("PC/SC", new PCSCReaderProvider());
            readerProviders.Add("STid STR", new STidSTRReaderProvider());
            readerProviders.Add("Rpleth", new RplethReaderProvider());

            foreach (string rpname in readerProviders.Keys)
            {
                cbReaderProvider.Items.Add(rpname);
            }
        }

        private void InitializeReaderUnitList()
        {
            readerUnits.Clear();
            cbReaderUnit.Items.Clear();

            if (cbReaderProvider.SelectedIndex > -1)
            {
                string rpkey = cbReaderProvider.SelectedItem.ToString();
                IReaderProvider readerProvider = readerProviders[rpkey];
                object[] rulist = (object[])readerProvider.GetReaderList();
                foreach (IReaderUnit ru in rulist)
                {
                    string name = ru.name;
                    if (!readerUnits.ContainsKey(name))
                    {
                        readerUnits.Add(name, ru);
                        cbReaderUnit.Items.Add(name);
                    }
                }
            }
        }

        private void cbReaderProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeReaderUnitList();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        public void SetConfiguration(DokanNFCConfig config)
        {
            rbtnModeRaw.Checked = (config.Mode == DisplayMode.Raw);
            rbtnModeNFC.Checked = (config.Mode == DisplayMode.NFC);
            cbReaderProvider.SelectedItem = config.ReaderProvider;
            cbReaderUnit.SelectedItem = config.ReaderUnit;
        }

        public DokanNFCConfig GetConfiguration()
        {
            DokanNFCConfig config = new DokanNFCConfig();

            if (rbtnModeRaw.Checked)
                config.Mode = DisplayMode.Raw;
            else if (rbtnModeNFC.Checked)
                config.Mode = DisplayMode.NFC;
            config.ReaderProvider = (cbReaderProvider.SelectedIndex > -1) ? cbReaderProvider.SelectedItem.ToString() : String.Empty;
            config.ReaderProvider = (cbReaderUnit.SelectedIndex > -1) ? cbReaderUnit.SelectedItem.ToString() : String.Empty;

            return config;
        }
    }
}

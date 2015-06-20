using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibLogicalAccess;

namespace DokanNFC
{
    public partial class DokanNFCConfigControl : UserControl
    {
        public DokanNFCConfigControl()
        {
            InitializeComponent();

            readerProviders = new Dictionary<string, IReaderProvider>();
        }

        Dictionary<string, IReaderProvider> readerProviders;

        private void DokanNFCConfigControl_Load(object sender, EventArgs e)
        {
            InitializeReaderProviderList();
            InitMountPoints();
            SetConfiguration(DokanNFCConfig.GetSingletonInstance());
        }

        private void InitMountPoints()
        {
            cbMountPoint.Items.Clear();
            cbMountPoint.Items.Add(String.Empty);
            cbMountPoint.Items.AddRange(DokanNFCConfig.GetAvailableMountPoints());
        }

        private void InitializeReaderProviderList()
        {
            readerProviders.Clear();
            cbReaderProvider.Items.Clear();

            readerProviders.Add(DokanNFCConfig.RP_PCSC, new PCSCReaderProvider());

            foreach (string rpname in readerProviders.Keys)
            {
                cbReaderProvider.Items.Add(rpname);
            }
        }

        private void InitializeReaderUnitList()
        {
            cbReaderUnit.Items.Clear();

            if (cbReaderProvider.SelectedIndex > -1)
            {
                string rpkey = cbReaderProvider.SelectedItem.ToString();
                IReaderProvider readerProvider = readerProviders[rpkey];
                object[] rulist = (object[])readerProvider.GetReaderList();
                cbReaderUnit.Items.Add(String.Empty);
                foreach (IReaderUnit ru in rulist)
                {
                    string name = ru.name;
                    if (!cbReaderUnit.Items.Contains(name))
                    {
                        cbReaderUnit.Items.Add(name);
                    }
                }
            }
        }

        private void cbReaderProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeReaderUnitList();
        }

        public void SetConfiguration(DokanNFCConfig config)
        {
            rbtnModeRaw.Checked = (config.Mode == DisplayMode.RawRFID);
            rbtnModeNFC.Checked = (config.Mode == DisplayMode.NFC);
            cbReaderProvider.SelectedItem = config.ReaderProvider;
            cbReaderUnit.SelectedItem = config.ReaderUnit;
            chkKeepMounted.Checked = config.AlwaysMounted;
            cbMountPoint.SelectedItem = config.MountPoint;
        }

        public DokanNFCConfig GetConfiguration()
        {
            DokanNFCConfig config = new DokanNFCConfig();

            if (rbtnModeRaw.Checked)
                config.Mode = DisplayMode.RawRFID;
            else if (rbtnModeNFC.Checked)
                config.Mode = DisplayMode.NFC;
            config.ReaderProvider = (cbReaderProvider.SelectedIndex > -1) ? cbReaderProvider.SelectedItem.ToString() : String.Empty;
            config.ReaderUnit = (cbReaderUnit.SelectedIndex > -1) ? cbReaderUnit.SelectedItem.ToString() : String.Empty;
            config.AlwaysMounted = chkKeepMounted.Checked;
            config.MountPoint = (cbMountPoint.SelectedIndex > -1) ? cbMountPoint.SelectedItem.ToString() : String.Empty;

            return config;
        }
    }
}

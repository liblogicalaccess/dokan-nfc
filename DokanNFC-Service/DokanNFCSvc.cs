using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DokanNFC
{
    public partial class DokanNFCSvc : ServiceBase
    {
        public DokanNFCSvc()
        {
            InitializeComponent();
            rfidListener = new RFIDListener();
        }

        RFIDListener rfidListener;

        protected override void OnStart(string[] args)
        {
            rfidListener.Start();
        }

        protected override void OnStop()
        {
            rfidListener.Stop();
        }
    }
}

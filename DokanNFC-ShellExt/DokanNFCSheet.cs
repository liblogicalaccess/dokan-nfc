using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DokanNFC
{
    public partial class DokanNFCSheet : SheetControl
    {
        public DokanNFCSheet()
        {
            InitializeComponent();

            SheetTitle = "NFC / RFID";
        }
    }
}

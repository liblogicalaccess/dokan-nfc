using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokanNFC
{
    public enum DisplayMode : int
    {
        Raw = 0,
        NFC = 1
    }

    [Serializable]
    public class DokanNFCConfig
    {
        public DokanNFCConfig()
        {
            Mode = DisplayMode.NFC;
            ReaderProvider = "PC/SC";
            ReaderUnit = String.Empty;
        }

        public DisplayMode Mode { get; set; }

        public string ReaderProvider { get; set; }

        public string ReaderUnit { get; set; }
    }
}

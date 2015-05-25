using LibLogicalAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DokanNFC
{
    public enum DisplayMode : int
    {
        RawRFID = 0,
        NFC = 1
    }

    [Serializable]
    public class DokanNFCConfig
    {
        public const string RP_PCSC = "PC/SC";

        public DokanNFCConfig()
        {
            Mode = DisplayMode.NFC;
            ReaderProvider = RP_PCSC;
            ReaderUnit = String.Empty;
            AlwaysMounted = true;
            CSNAsRoot = false;
        }

        public DisplayMode Mode { get; set; }

        public string ReaderProvider { get; set; }

        public string ReaderUnit { get; set; }

        public bool AlwaysMounted { get; set; }

        public bool CSNAsRoot { get; set; }

        public string MountPoint { get; set; }

        public static string FileNamePath
        {
            get
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                path = System.IO.Path.Combine(path, "ISLOG\\DokanNFC");
                return path;
            }
        }

        public static string FileName
        {
            get
            {
                return System.IO.Path.Combine(FileNamePath, "configuration.xml");
            }
        }

        private static DokanNFCConfig instance = null;
        public static DokanNFCConfig GetSingletonInstance()
        {
            if (instance == null)
            {
                instance = GetInstanceFromFile();
            }

            return instance;
        }

        public static DokanNFCConfig GetInstanceFromFile()
        {
            return GetInstanceFromFile(FileName);
        }

        public static DokanNFCConfig GetInstanceFromFile(string fileName)
        {
            DokanNFCConfig config = new DokanNFCConfig();
            if (File.Exists(FileName))
            {
                XmlSerializer xs = new XmlSerializer(typeof(DokanNFCConfig));
                using (StreamReader rr = new StreamReader(FileName))
                {
                    config = xs.Deserialize(rr) as DokanNFCConfig;
                }
            }
            return config;
        }

        public void SaveToFile()
        {
            if (!Directory.Exists(FileNamePath))
                Directory.CreateDirectory(FileNamePath);

            XmlSerializer xs = new XmlSerializer(typeof(DokanNFCConfig));
            using (StreamWriter wr = new StreamWriter(FileName))
            {
                xs.Serialize(wr, this);
            }
        }

        public static IReaderProvider CreateReaderProviderFromName(string name)
        {
            IReaderProvider readerProvider = null;

            switch (name)
            {
                case RP_PCSC:
                    readerProvider = new PCSCReaderProvider();
                    break;
            }

            return readerProvider;
        }

        public static IReaderUnit CreateReaderUnitFromName(IReaderProvider readerProvider, string name)
        {
            IReaderUnit readerUnit = null;

            if (readerProvider != null)
            {
                readerUnit = readerProvider.CreateReaderUnit();
                switch (readerProvider.RPType)
                {
                    case RP_PCSC:
                        {
                            IPCSCReaderUnit ru = readerUnit as IPCSCReaderUnit;
                            ru.SetName(name);
                        }
                        break;
                }
            }

            return readerUnit;
        }

        public static string[] GetAvailableMountPoints()
        {
            List<char> driveLetters = new List<char>(24);
            // increment from ASCII values for C-Z
            for (int i = 67; i < 91; i++)
            {
                driveLetters.Add(Convert.ToChar(i));
            }
            // removed used drive letters from possible drive letters
            foreach (string drive in Directory.GetLogicalDrives())
            {
                driveLetters.Remove(drive[0]);
            }

            string[] mountPoints = new string[driveLetters.Count];
            for (int i = 0; i < driveLetters.Count; ++i)
            {
                mountPoints[i] = driveLetters[i] + ":\\";
            }
            return mountPoints;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokanNFC
{
    public class FileCache
    {
        public FileCache()
        {
            Data = new byte[0];
            LastModificationDate = LastAccessDate = DateTime.Now;
        }

        public byte[] Data { get; set; }

        public DateTime LastModificationDate { get; set; }

        public DateTime LastAccessDate { get; set; }
    }
}

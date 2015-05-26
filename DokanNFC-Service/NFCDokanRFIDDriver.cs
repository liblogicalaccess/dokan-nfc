using DokanNet;
using LibLogicalAccess;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FileAccess = DokanNet.FileAccess;

namespace DokanNFC
{
    public class NFCDokanRFIDDriver : DokanRFIDDriver
    {
        public NFCDokanRFIDDriver(RFIDListener rfidListener)
            : base(rfidListener)
        {
            
        }

        public override DokanError CreateDirectory(string fileName, DokanFileInfo info)
        {
            log.Info(String.Format("CreateDirectory call - {0}", fileName));

            return DokanError.ErrorError;
        }

        public override DokanError CreateFile(string fileName, FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, DokanFileInfo info)
        {
            log.Info(String.Format("CreateFile call - {0}", fileName));

            IChip chip = rfidListener.GetChip();
            if (chip == null)
                return DokanError.ErrorNotReady;

            if (fileName == "\\" || fileName == ("\\" + chip.ChipIdentifier))
            {
                return DokanError.ErrorSuccess;
            }
            else if ((attributes & FileAttributes.Directory) != 0)
            {
                return DokanError.ErrorFileNotFound;
            }
            else
            {
                bool exists = false;
                lock (cacheFiles)
                {
                    if (!CacheExists(fileName))
                    {
                        if (CacheCount() > 0)
                        {
                            log.Error("Cache already initiliazed and cannot found file");
                            return DokanError.ErrorFileNotFound;
                        }

                        DokanError payloadRet = ReadAndCachePayload(nfcConfig.CSNAsRoot ? "\\" + chip.ChipIdentifier : String.Empty);
                        if (payloadRet == DokanError.ErrorSuccess)
                        {
                            exists = CacheExists(fileName);
                        }
                        
                        return payloadRet;
                    }
                    else
                    {
                        exists = true;
                    }
                }

                log.Info(String.Format("Exists? {0}", exists));

                switch (mode)
                {
                    case FileMode.Open:
                        {
                            log.Info("FileMode Open");
                            if (exists)
                                return DokanError.ErrorSuccess;
                            else
                                return DokanError.ErrorFileNotFound;
                        }

                    case FileMode.CreateNew:
                        {
                            log.Info("FileMode CreateNew");
                            if (exists)
                                return DokanError.ErrorAlreadyExists;

                            InitCache(fileName);
                            return DokanError.ErrorSuccess;
                        }

                    case FileMode.Create:
                        {
                            log.Info("FileMode Create");
                            InitCache(fileName);
                            return DokanError.ErrorSuccess;
                        }

                    case FileMode.OpenOrCreate:
                        {
                            log.Info("FileMode OpenOrCreate");
                            if (!exists)
                            {
                                InitCache(fileName);
                            }
                            return DokanError.ErrorSuccess;
                        }

                    case FileMode.Truncate:
                        {
                            log.Info("FileMode Truncate");
                            if (!exists)
                                return DokanError.ErrorFileNotFound;

                            InitCache(fileName);
                            return DokanError.ErrorSuccess;
                        }

                    case FileMode.Append:
                        {
                            log.Info("FileMode Append");
                            if (!exists)
                                return DokanError.ErrorFileNotFound;

                            return DokanError.ErrorSuccess;
                        }
                    default:
                        {
                            log.Error(String.Format("Error unknown FileMode {0}", mode));
                            return DokanError.ErrorError;
                        }
                }
            }
        }

        public override DokanError DeleteDirectory(string fileName, DokanFileInfo info)
        {
            log.Info(String.Format("DeleteDirectory call - {0}", fileName));
            
            // Deleting root directory = card format
            if (fileName.IndexOf('\\') == -1)
            {
                EraseCard();
            }
            
            return DokanError.ErrorPathNotFound;
        }

        public override DokanError DeleteFile(string fileName, DokanFileInfo info)
        {
            log.Info(String.Format("DeleteFile call - {0}", fileName));

            if (!CacheExists(fileName))
                return DokanError.ErrorFileNotFound;

            // Only one NDEF record supported for now, removing a file = removing complete NDEF message = card format
            return EraseCard();
        }

        public override DokanError FindFiles(string fileName, out IList<FileInformation> files, DokanFileInfo info)
        {
            log.Info(String.Format("FindFiles call - {0}", fileName));

            files = new List<FileInformation>();

            IChip chip = rfidListener.GetChip();
            if (chip == null)
                return DokanError.ErrorNotReady;

            if (fileName == "\\")
            {
                if (nfcConfig.CSNAsRoot)
                    return PopulateCSNFiles(files);
                
                return PopulateNDEFFiles(files);
            }
            else
            {
                log.Info("Subdir");

                if (!nfcConfig.CSNAsRoot)
                    return DokanError.ErrorPathNotFound;

                if (fileName != chip.ChipIdentifier)
                    return DokanError.ErrorPathNotFound;

                return PopulateNDEFFiles(files, fileName);
            }
        }

        protected DokanError PopulateCSNFiles(IList<FileInformation> files)
        {
            log.Info("PopulateCSNFiles call");

            IChip chip = rfidListener.GetChip();
            if (chip == null)
                return DokanError.ErrorNotReady;

            FileInformation csnFile = new FileInformation();
            csnFile.Attributes = FileAttributes.Directory;
            csnFile.FileName = chip.ChipIdentifier;
            csnFile.CreationTime = csnFile.LastWriteTime = csnFile.LastAccessTime = rfidListener.GetChipInsertionDate();
            csnFile.Length = 0;
            files.Add(csnFile);
            return DokanError.ErrorSuccess;
        }

        protected DokanError PopulateNDEFFiles(IList<FileInformation> files, string parentName = null)
        {
            log.Info("PopulateNDEFFiles call");

            if (CacheCount() == 0)
            {
                log.Info("No cache yet, building cache list...");
                DokanError payloadRet = ReadAndCachePayload(parentName);
                if (payloadRet != DokanError.ErrorSuccess)
                    return payloadRet;
            }

            foreach (string filename in cacheFiles.Keys)
            {
                log.Info(String.Format("Found file {0}", filename));
                FileInformation ndefFile = new FileInformation();
                ndefFile.Attributes = FileAttributes.Normal;
                ndefFile.FileName = Path.GetFileName(filename);
                ndefFile.CreationTime = ndefFile.LastWriteTime = cacheFiles[filename].LastModificationDate;
                ndefFile.LastAccessTime = cacheFiles[filename].LastAccessDate;
                ndefFile.Length = cacheFiles[filename].Data.Length;
                files.Add(ndefFile);
            }
            return DokanError.ErrorSuccess;
        }

        protected DokanError ReadAndCachePayload(string parentName)
        {
            byte[] payload = null;
            string extension = String.Empty;
            DokanError payloadRet = ReadPayload(out payload, out extension);
            if (payloadRet != DokanError.ErrorSuccess)
            {
                log.Error("Error reading payload");
                return payloadRet;
            }

            string filename = "\\record";
            if (!String.IsNullOrEmpty(extension))
                filename += "." + extension;

            if (!String.IsNullOrEmpty(parentName))
            {
                filename = parentName + filename;
            }
            InitCache(filename);
            WriteToCache(filename, payload);
            log.Info(String.Format("Record cached as {0}", filename));
            return DokanError.ErrorSuccess;
        }

        protected DokanError ReadPayload(out byte[] payload, out string extension)
        {
            payload = null;
            extension = String.Empty;

            // Make sure the chip is still here
            if (!rfidListener.ReconnectOnCard())
            {
                log.Error("Card reconnection failed");
                return DokanError.ErrorNotReady;
            }

            IChip chip = rfidListener.GetChip();
            ICardService svc = chip.GetService(CardServiceType.CST_NFC_TAG);
            IDESFireEV1NFCTag4CardService nfcsvc = svc as IDESFireEV1NFCTag4CardService;
            if (nfcsvc == null)
            {
                log.Error("No NFC service.");
                // If no NFC service for this chip, we must fail
                return DokanError.ErrorNotReady;
            }

            try
            {
                NdefMessage ndef = nfcsvc.ReadNDEFFile();
                if (ndef != null)
                {
                    if (ndef.GetRecordCount() > 0)
                    {
                        // For now, only support for one Ndef record per card
                        object[] records = ndef.Records as object[];
                        if (records != null)
                        {
                            INdefRecord record = records[0] as INdefRecord;
                            object payloadobj = record.Payload;
                            if (payloadobj != null)
                            {
                                log.Info("Waza4");
                                payload = payloadobj as byte[];
                                log.Info("Waza5");
                                extension = GetPayloadExtension(record);
                            }
                            else
                            {
                                log.Error("Bad record payload");
                            }
                        }
                    }
                    else
                    {
                        log.Info("No NDEF record in the message");
                    }
                }
                else
                {
                    log.Info("No NDEF message on ths card");
                }
            }
            catch (COMException ex)
            {
                log.Info("NDEF read error", ex);
            }

            log.Info("WazaX");
            return DokanError.ErrorSuccess;
        }

        public override long GetFileSize(string fileName)
        {
            IChip chip = rfidListener.GetChip();
            byte[] data = ReadFromCache(fileName);
            if (data != null)
                return data.Length;

            return 0;
        }

        protected string GetPayloadExtension(INdefRecord record)
        {
            string ext = String.Empty;
            object otype = record.Type;
            if (otype != null)
            {
                byte[] type = (byte[])otype;

                if (type.Length == 1 && type[0] == 0x54)
                {
                    // Text record type
                    ext = "txt";
                }
                else if (type.Length == 1 && type[0] == 0x55)
                {
                    // Uri record type
                    ext = "url";
                }
                else if (record.TNF == TNF.TNF_MIME_MEDIA)
                {
                    string mimeType = System.Text.Encoding.UTF8.GetString(type).ToLower();
                    ext = GetDefaultExtension(mimeType);                    
                }
            }

            return ext;
        }

        protected string GetDefaultExtension(string mimeType)
        {
            string result;
            RegistryKey key;
            object value;

            key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimeType, false);
            value = key != null ? key.GetValue("Extension", null) : null;
            result = value != null ? value.ToString() : String.Empty;

            return result;
        }

        public override DokanError MoveFile(string oldName, string newName, bool replace, DokanFileInfo info)
        {
            log.Info(String.Format("MoveFile call - {0} to {1}", oldName, newName));

            return DokanError.ErrorError;
        }

        public override DokanError OpenDirectory(string fileName, DokanFileInfo info)
        {
            log.Info(String.Format("OpenDirectory call - {0}", fileName));

            IChip chip = rfidListener.GetChip();
            if (chip == null)
                return DokanError.ErrorNotReady;

            if (fileName == "\\")
                return DokanError.ErrorSuccess;

            if (nfcConfig.CSNAsRoot)
            {
                if (fileName == chip.ChipIdentifier)
                    return DokanError.ErrorSuccess;
            }

            log.Error("Path not found");
            return DokanError.ErrorPathNotFound;
        }

        public override DokanError ReadFile(string fileName, byte[] buffer, out int bytesRead, long offset, DokanFileInfo info)
        {
            log.Info(String.Format("ReadFile call - {0}, Offset: {1}, Length: {2}", fileName, offset, buffer.Length));
            bytesRead = 0;

            if (info.IsDirectory)
            {
                log.Info("Is directory, skipped");
                return DokanError.ErrorError;
            }

            // For NFC we only support one NDEF record for now, no need to check fileName then
            byte[] data = ReadFromCache(fileName, buffer.Length, (int)offset);
            if (data == null)
            {
                log.Error("No data");
                return DokanError.ErrorError;
            }

            log.Info(String.Format("{0} bytes read", data.Length));
            Array.Copy(data, buffer, data.Length);
            bytesRead = data.Length;
            return DokanError.ErrorSuccess;
        }

        public override DokanError WriteFile(string fileName, byte[] buffer, out int bytesWritten, long offset, DokanFileInfo info)
        {
            log.Info(String.Format("WriteFile call - {0}, Offset: {1}, Length: {2}", fileName, offset, buffer.Length));
            bytesWritten = 0;

            if (info.IsDirectory)
            {
                log.Info("Is directory, skipped");
                return DokanError.ErrorError;
            }

            if (!CacheExists(fileName))
            {
                log.Error("No existing cache");
                return DokanError.ErrorError;
            }

            WriteToCache(fileName, buffer, (int)offset);
            bytesWritten = buffer.Length;
            return DokanError.ErrorSuccess;
        }
    }
}

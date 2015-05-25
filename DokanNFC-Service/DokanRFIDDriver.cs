using DokanNet;
using LibLogicalAccess;
using log4net;
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
    public abstract class DokanRFIDDriver : IDokanOperations
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(DokanRFIDDriver));

        public DokanRFIDDriver(RFIDListener rfidListener)
        {
            this.rfidListener = rfidListener;
            this.cacheFiles = new Dictionary<string, FileCache>();
            this.nfcConfig = DokanNFCConfig.GetSingletonInstance();
        }

        protected RFIDListener rfidListener;
        /// <summary>
        /// Cache of files data.
        /// </summary>
        protected Dictionary<string, FileCache> cacheFiles;
        protected DokanNFCConfig nfcConfig;

        protected bool CacheExists(string filename)
        {
            lock (cacheFiles)
            {
                return cacheFiles.ContainsKey(filename);
            }
        }

        protected int CacheCount()
        {
            lock (cacheFiles)
            {
                return cacheFiles.Count;
            }
        }

        protected void InitCache(string filename)
        {
            lock (cacheFiles)
            {
                if (cacheFiles.ContainsKey(filename))
                    cacheFiles.Remove(filename);
                cacheFiles.Add(filename, new FileCache());
            }
        }

        protected void WriteToCache(string filename, byte[] data, int offset = 0)
        {
            filename = filename.ToLower();
            lock (cacheFiles)
            {
                if (!cacheFiles.ContainsKey(filename))
                    InitCache(filename);
            }

            int end = offset + data.Length;
            if (end > cacheFiles[filename].Data.Length)
            {
                byte[] cache = cacheFiles[filename].Data;
                Array.Resize(ref cache, end);
                cacheFiles[filename].Data = cache;
                cacheFiles[filename].LastModificationDate = cacheFiles[filename].LastAccessDate = DateTime.Now;
            }
            Array.Copy(data, 0, cacheFiles[filename].Data, offset, data.Length);
        }

        protected byte[] ReadFromCache(string filename, int length = 0, int offset = 0)
        {
            filename = filename.ToLower();
            if (!cacheFiles.ContainsKey(filename))
                return null;

            if (length == 0)
                length = cacheFiles[filename].Data.Length;

            byte[] chunk = new byte[length];
            Array.Copy(cacheFiles[filename].Data, offset, chunk, 0, length);
            cacheFiles[filename].LastAccessDate = DateTime.Now;
            return chunk;
        }

        internal void ResetCache()
        {
            lock (cacheFiles)
            {
                cacheFiles.Clear();
            }
        }

        protected DokanError EraseCard()
        {
            try
            {
                IChip chip = rfidListener.GetChip();
                if (chip == null)
                {
                    return DokanError.ErrorNotReady;
                }

                IStorageCardService storage = chip.GetService(CardServiceType.CST_STORAGE) as IStorageCardService;
                if (storage != null)
                {
                    storage.Erase();
                    return DokanError.ErrorSuccess;
                }
                else
                {
                    return DokanError.ErrorNotImplemented;
                }
            }
            catch (Exception ex)
            {
                log.Error("DeleteDirectory error", ex);
                rfidListener.ResetCard();
                return DokanError.ErrorError;
            }
        }

        public DokanError Cleanup(string fileName, DokanFileInfo info)
        {
            log.Info(String.Format("Cleanup call - {0}", fileName));

            return DokanError.ErrorSuccess;
        }

        public DokanError CloseFile(string fileName, DokanFileInfo info)
        {
            log.Info(String.Format("CloseFile call - {0}", fileName));
            
            return DokanError.ErrorSuccess;
        }

        public abstract DokanError CreateDirectory(string fileName, DokanFileInfo info);

        public abstract DokanError CreateFile(string fileName, FileAccess access, System.IO.FileShare share, System.IO.FileMode mode, System.IO.FileOptions options, System.IO.FileAttributes attributes, DokanFileInfo info);

        public abstract DokanError DeleteDirectory(string fileName, DokanFileInfo info);

        public abstract DokanError DeleteFile(string fileName, DokanFileInfo info);

        public abstract DokanError FindFiles(string fileName, out IList<FileInformation> files, DokanFileInfo info);

        public DokanError FlushFileBuffers(string fileName, DokanFileInfo info)
        {
            log.Info(String.Format("FlushFileBuffers call - {0}", fileName));

            return DokanError.ErrorSuccess;
        }

        public DokanError GetDiskFreeSpace(out long free, out long total, out long used, DokanFileInfo info)
        {
            log.Info("GetDiskFreeSpace call");

            free = 0;
            total = 0;
            used = 0;

            try
            {
                IChip chip = rfidListener.GetChip();
                if (chip == null)
                {
                    return DokanError.ErrorNotReady;
                }

                if (chip.Type == "Mifare1K")
                {
                    free = 0;
                    total = 32 + (15 * 3 * 16);
                    used = total;
                }
                else if (chip.Type == "Mifare4K")
                {
                    free = 0;
                    total = 32 + (30 * 48) + (8 * 240);
                    used = total;
                }
                else if (chip.Type == "DESFire" || chip.Type == "DESFireEV1")
                {
                    free = 0;
                    total = 0;
                    used = 0;

                    IDESFireEV1Commands cmd = chip.Commands as IDESFireEV1Commands;
                    if (cmd != null)
                    {
                        try
                        {
                            free = cmd.GetFreeMemory() / 1024;
                            DESFireCardVersion version = cmd.GetVersion();
                            total = ((long)(version.softwareStorageSize / 4)) * 1024;
                            used = total - free;
                        }
                        catch (COMException) { }
                    }
                }
            }
            catch(Exception ex)
            {
                log.Error("GetDiskFreeSpace error", ex);
                return DokanError.ErrorError;
            }

            return DokanError.ErrorSuccess;
        }

        public abstract long GetFileSize(string fileName);

        public DokanError GetFileInformation(string fileName, out FileInformation fileInfo, DokanFileInfo info)
        {
            log.Info(String.Format("GetFileInformation call - {0}", fileName));

            fileInfo = new FileInformation();
            try
            {
                DateTime org = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                fileInfo.CreationTime = org;
                fileInfo.LastAccessTime = org;
                fileInfo.LastWriteTime = org;
                fileInfo.Length = GetFileSize(fileName);
            }
            catch (FileNotFoundException)
            {
                return DokanError.ErrorFileNotFound;
            }
            catch (Exception ex)
            {
                log.Error("GetFileInformation error", ex);
                rfidListener.ResetCard();
                return DokanError.ErrorError;
            }

            return DokanError.ErrorSuccess;
        }

        public DokanError GetFileSecurity(string fileName, out System.Security.AccessControl.FileSystemSecurity security, System.Security.AccessControl.AccessControlSections sections, DokanFileInfo info)
        {
            log.Info(String.Format("GetFileSecurity call - {0}", fileName));

            security = null;
            return DokanError.ErrorError;
        }

        public DokanError GetVolumeInformation(out string volumeLabel, out FileSystemFeatures features, out string fileSystemName, DokanFileInfo info)
        {
            log.Info("GetVolumeInformation call");

            volumeLabel = nfcConfig.ReaderUnit;
            features = FileSystemFeatures.None;
            fileSystemName = Enum.GetName(typeof(DisplayMode), nfcConfig.Mode);

            return DokanError.ErrorSuccess;
        }

        public DokanError LockFile(string fileName, long offset, long length, DokanFileInfo info)
        {
            log.Info(String.Format("LockFile call - {0}", fileName));

            return DokanError.ErrorSuccess;
        }

        public abstract DokanError MoveFile(string oldName, string newName, bool replace, DokanFileInfo info);

        public abstract DokanError OpenDirectory(string fileName, DokanFileInfo info);

        public abstract DokanError ReadFile(string fileName, byte[] buffer, out int bytesRead, long offset, DokanFileInfo info);

        public DokanError SetAllocationSize(string fileName, long length, DokanFileInfo info)
        {
            log.Info(String.Format("SetAllocationSize call - {0}", fileName));

            return DokanError.ErrorSuccess;
        }

        public DokanError SetEndOfFile(string fileName, long length, DokanFileInfo info)
        {
            log.Info(String.Format("SetEndOfFile call - {0}", fileName));

            return DokanError.ErrorSuccess;
        }

        public DokanError SetFileAttributes(string fileName, System.IO.FileAttributes attributes, DokanFileInfo info)
        {
            log.Info(String.Format("SetFileAttributes call - {0}", fileName));

            return DokanError.ErrorError;
        }

        public DokanError SetFileSecurity(string fileName, System.Security.AccessControl.FileSystemSecurity security, System.Security.AccessControl.AccessControlSections sections, DokanFileInfo info)
        {
            log.Info(String.Format("SetFileSecurity call - {0}", fileName));

            return DokanError.ErrorError;
        }

        public DokanError SetFileTime(string fileName, DateTime? creationTime, DateTime? lastAccessTime, DateTime? lastWriteTime, DokanFileInfo info)
        {
            log.Info(String.Format("SetFileTime call - {0}", fileName));

            return DokanError.ErrorError;
        }

        public DokanError UnlockFile(string fileName, long offset, long length, DokanFileInfo info)
        {
            log.Info(String.Format("UnlockFile call - {0}", fileName));

            return DokanError.ErrorSuccess;
        }

        public DokanError Unmount(DokanFileInfo info)
        {
            log.Info("Unmount call");

            rfidListener.ResetCard();
            return DokanError.ErrorSuccess;
        }

        public abstract DokanError WriteFile(string fileName, byte[] buffer, out int bytesWritten, long offset, DokanFileInfo info);
    }
}

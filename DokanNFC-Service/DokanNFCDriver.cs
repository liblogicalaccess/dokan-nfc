using DokanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokanNFC
{
    public class DokanNFCDriver : IDokanOperations
    {
        public DokanError Cleanup(string fileName, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError CloseFile(string fileName, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError CreateDirectory(string fileName, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError CreateFile(string fileName, FileAccess access, System.IO.FileShare share, System.IO.FileMode mode, System.IO.FileOptions options, System.IO.FileAttributes attributes, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError DeleteDirectory(string fileName, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError DeleteFile(string fileName, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError FindFiles(string fileName, out IList<FileInformation> files, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError FlushFileBuffers(string fileName, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError GetDiskFreeSpace(out long free, out long total, out long used, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError GetFileInformation(string fileName, out FileInformation fileInfo, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError GetFileSecurity(string fileName, out System.Security.AccessControl.FileSystemSecurity security, System.Security.AccessControl.AccessControlSections sections, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError GetVolumeInformation(out string volumeLabel, out FileSystemFeatures features, out string fileSystemName, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError LockFile(string fileName, long offset, long length, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError MoveFile(string oldName, string newName, bool replace, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError OpenDirectory(string fileName, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError ReadFile(string fileName, byte[] buffer, out int bytesRead, long offset, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError SetAllocationSize(string fileName, long length, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError SetEndOfFile(string fileName, long length, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError SetFileAttributes(string fileName, System.IO.FileAttributes attributes, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError SetFileSecurity(string fileName, System.Security.AccessControl.FileSystemSecurity security, System.Security.AccessControl.AccessControlSections sections, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError SetFileTime(string fileName, DateTime? creationTime, DateTime? lastAccessTime, DateTime? lastWriteTime, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError UnlockFile(string fileName, long offset, long length, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError Unmount(DokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public DokanError WriteFile(string fileName, byte[] buffer, out int bytesWritten, long offset, DokanFileInfo info)
        {
            throw new NotImplementedException();
        }
    }
}

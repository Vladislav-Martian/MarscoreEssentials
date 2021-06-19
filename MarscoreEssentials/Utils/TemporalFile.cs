using System;
using System.IO;

namespace MarscoreEssentials.Utils
{
    public class TemporalFile : IDisposable
    {
        private string _filePath;
        private FileInfo _fileInfo;
        public TemporalFile()
        {
            _filePath = Path.GetTempFileName();
            _fileInfo = new FileInfo(_filePath);
        }
        public string FilePath => _filePath;

        private void ReleaseUnmanagedResources()
        {
            _fileInfo.Delete();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
        }

        ~TemporalFile()
        {
            ReleaseUnmanagedResources();
        }

        public FileStream Open(
            System.IO.FileMode mode = FileMode.Open, 
            System.IO.FileAccess access = FileAccess.ReadWrite, 
            System.IO.FileShare share = FileShare.ReadWrite, 
            int bufferSize = 4096, 
            bool useAsync = false)
        {
            return new FileStream(_filePath, mode, access, share, bufferSize, useAsync);
        }
    }
}
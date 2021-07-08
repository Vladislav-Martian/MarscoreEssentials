using System;
using System.Collections.Generic;
using System.IO;

namespace MarscoreEssentials.Memory
{
    public sealed class TemporalFile: IDisposable
    {
        #region Props
        public string Name { get; }
        public string Path { get; }
        public string FolderPath
        {
            get => System.IO.Path.GetDirectoryName(Path);
        }

        public FileStream Stream { get; private set; } = null;

        private TemporalFolder TemporalFolderHook { get; }
        #endregion
        
        #region Static
        private static List<TemporalFile> All { get; }
        static TemporalFile()
        {
            All = new List<TemporalFile>();
        }
        public static string DefaultTemporalFileExtension { get; set; } = ".tmp";

        public static void Clear()
        {
            var tmp = All.ToArray();
            All.Clear();
            foreach (var file in All)
            {
                file.Dispose();
            }
        }
        #endregion

        #region Structural
        public TemporalFile(in string name = null, in string dPath = null, in TemporalFolder hook = null)
        {
            Name = name ?? Guid.NewGuid().ToString() + DefaultTemporalFileExtension; 
            Path = System.IO.Path.Combine(
                dPath ?? System.IO.Path.GetTempPath(), 
                Name);
            if (!File.Exists(Name))
            {
                File.Create(Name).Close();
            }
            All.Add(this);
            TemporalFolderHook = hook;
        }
        
        public void Dispose()
        {
            Close();
            All.Remove(this);
            File.Delete(Path);
            TemporalFolderHook?._files.Remove(Name);
        }
        ~TemporalFile()
        {
            Dispose();
        }
        #endregion

        #region Other
        public FileStream Open(
            System.IO.FileMode mode = FileMode.OpenOrCreate, 
            System.IO.FileAccess access = FileAccess.ReadWrite, 
            System.IO.FileShare share = FileShare.ReadWrite, 
            int bufferSize = 4096, 
            bool useAsync = false)
        {
            return Stream ??= new FileStream(Path, mode, access, share, bufferSize, useAsync);
        }

        public void Close()
        {
            Stream.Close();
        }
        #endregion

        
    }
}
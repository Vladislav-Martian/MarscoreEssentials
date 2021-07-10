using System;
using System.Collections.Generic;
using System.IO;

namespace Marscore.Memory
{
    /// <summary>
    /// Represents temporal file. Can be used in using statement. On first file open saves FileStream
    /// to poperty Stream. Stream can be closed directly on tf object by Close(), or will be closed
    /// on Dispose() after using, but you need to use Open() to create Stream.;
    /// </summary>
    public sealed class TemporalFile: IDisposable
    {
        #region Props
        /// <summary>
        /// Only file name and extension, not path!!!
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Full path to file
        /// </summary>
        public string Path { get; }
        /// <summary>
        /// Path to folder, where file is located
        /// </summary>
        public string FolderPath
        {
            get => System.IO.Path.GetDirectoryName(Path);
        }
        /// <summary>
        /// Null if tf instance never opened. After Open() contains FileStream instance.
        /// Can be closed directly on tf instance.
        /// </summary>
        public FileStream Stream { get; private set; } = null;
        /// <summary>
        /// Used only inside, if temporal file placed inside temporal folder from class TemporalFolder.
        /// Needed to safely remove file frome folder and dictionary inside TemporalFolder instance.
        /// </summary>
        private TemporalFolder TemporalFolderHook { get; }
        #endregion
        
        #region Static
        /// <summary>
        /// Contains all ever created temporal files
        /// </summary>
        private static List<TemporalFile> All { get; }
        static TemporalFile()
        {
            All = new List<TemporalFile>();
        }
        /// <summary>
        /// Extension that has every default temp file. Default: .tmp
        /// </summary>
        public static string DefaultTemporalFileExtension { get; set; } = ".tmp";

        /// <summary>
        /// Clears all created instances and removes files.
        /// </summary>
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
        /// <summary>
        /// Default constructor. Use named argument passing if need to change only second or third argument.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dPath"></param>
        /// <param name="hook"></param>
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
        /// <summary>
        /// Closes FileStream in Steam property
        /// Removes object feom All collection
        /// Removes file itself
        /// Removes object from linked TemporalFolder hook.
        /// </summary>
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
        /// <summary>
        /// Open file and put stream to property Stream
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="access"></param>
        /// <param name="share"></param>
        /// <param name="bufferSize"></param>
        /// <param name="useAsync"></param>
        /// <returns></returns>
        public FileStream Open(
            System.IO.FileMode mode = FileMode.OpenOrCreate, 
            System.IO.FileAccess access = FileAccess.ReadWrite, 
            System.IO.FileShare share = FileShare.ReadWrite, 
            int bufferSize = 4096, 
            bool useAsync = false)
        {
            return Stream ??= new FileStream(Path, mode, access, share, bufferSize, useAsync);
        }

        /// <summary>
        /// Closes Stream
        /// </summary>
        public void Close()
        {
            Stream?.Close();
        }
        #endregion

        
    }
}
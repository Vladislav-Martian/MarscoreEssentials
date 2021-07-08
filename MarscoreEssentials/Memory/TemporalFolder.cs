using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarscoreEssentials.Memory
{
    public sealed class TemporalFolder: IDisposable
    {
        #region Props

        /// <summary>
        /// Only folder name, not full path
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Full folder path
        /// </summary>
        public string Path { get; }
        /// <summary>
        /// Files inside
        /// </summary>
        internal Dictionary<string, TemporalFile> _files{ get; }
        #endregion

        #region Static
        /// <summary>
        /// All ever created tfo
        /// </summary>
        private static List<TemporalFolder> All { get; }
        
        static TemporalFolder()
        {
            All = new List<TemporalFolder>();
        }
        /// <summary>
        /// Removes all temporal folders
        /// </summary>
        public static void Clear()
        {
            var tmp = All.ToArray();
            All.Clear();
            foreach (var folder in tmp)
            {
                folder.Dispose();
            }
        }

        #endregion

        #region Structural
        /// <summary>
        /// Default constructor, use named argument passing
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dPath"></param>
        public TemporalFolder(in string name = null, in string dPath = null)
        {
            Path = System.IO.Path.Combine(
                dPath ?? System.IO.Path.GetTempPath(),
                name ?? Guid.NewGuid().ToString());
            All.Add(this);
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            _files = new Dictionary<string, TemporalFile>();
        }
        /// <summary>
        /// Remove from All,
        /// Dispose all tmp files inside (and close it streams)
        /// Delete files
        /// Delete folder
        /// </summary>
        public void Dispose()
        {
            All.Remove(this);
            foreach (var file in _files)
            {
                file.Value.Dispose();
            }
            Directory.Delete(Path);
        }
        ~TemporalFolder()
        {
            Dispose();
        }
        #endregion

        #region Other
        /// <summary>
        /// Create file inside tfo
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TemporalFile CreateTemporalFile(in string name = null)
        {
            var tmp = new TemporalFile(name, Path, this);
            _files.Add(tmp.Name, tmp);
            return tmp;
        }
        /// <summary>
        /// Get file by [] operator, with filename and extension in one string
        /// </summary>
        /// <param name="key"></param>
        public TemporalFile this[string key]
        {
            get => _files[key];
        }
        /// <summary>
        /// Get file by index in dictionary
        /// </summary>
        /// <param name="index"></param>
        public TemporalFile this[int index]
        {
            get => _files.Values.ToArray()[index];
        }

        #endregion
    }
}
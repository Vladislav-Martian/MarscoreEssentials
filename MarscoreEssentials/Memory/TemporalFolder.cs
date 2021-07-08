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
        public string Path { get; }
        internal Dictionary<string, TemporalFile> _files{ get; }
        #endregion

        #region Static
        private static List<TemporalFolder> All { get; }

        static TemporalFolder()
        {
            All = new List<TemporalFolder>();
        }

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

        public TemporalFile CreateTemporalFile(in string name = null)
        {
            var tmp = new TemporalFile(name, Path, this);
            _files.Add(tmp.Name, tmp);
            return tmp;
        }

        public TemporalFile this[string key]
        {
            get => _files[key];
        }
        public TemporalFile this[int index]
        {
            get => _files[_files.Keys.ToArray()[index]];
        }

        #endregion
    }
}
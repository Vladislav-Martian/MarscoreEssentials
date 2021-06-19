using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace MarscoreEssentials.Utils
{
    /// <summary>
    /// Class called to simplify opening files and folders.
    /// </summary>
    public sealed class PathHelper
    {
        public string InitialDirectoryPath { get; private set; }

        /// <summary>
        /// Start point directory. Use something like:
        /// For faster working class don`t test given string on right path!!!
        /// <code>
        /// var PH = new PathHelper("C:\Some\Folder\Path");
        /// </code>
        /// </summary>
        public PathHelper(in string path)
        {
            InitialDirectoryPath = path;
        }
        
        /// <summary>
        /// Alternative constructor, uses assembly location.
        /// Just create object using executing assembly of your program
        /// </summary>
        /// <param name="assembly"></param>
        /// <code>
        /// System.Reflection.Assembly.GetExecutingAssembly()
        /// </code> 
        public PathHelper(in Assembly assembly)
        {
            InitialDirectoryPath = Path.GetDirectoryName(
                Uri.UnescapeDataString(
                    new UriBuilder(assembly.CodeBase).Path));
        }

        #region Navigate

        #region NavigatorOptimization
        /// <summary>
        /// dictionary that implements tree of PathHelper objects
        /// </summary>
        private Dictionary<string, PathHelper> InternalTree = new Dictionary<string, PathHelper>();

        /// <summary>
        /// Return full path to some file.
        /// Argument just "contains name.extension"
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string FilePath(string filename)
        {
            return Path.Join(InitialDirectoryPath, filename);
        }

        #endregion
        /// <summary>
        /// Returns next folder tree element by name of nested folder.
        /// </summary>
        /// <param name="name"></param>
        public object this[string name]
        {
            get
            {
                if (InternalTree.ContainsKey(name))
                {
                    return InternalTree[name];
                }
                InternalTree[name] = new PathHelper(
                    Path.Join(
                        InitialDirectoryPath, name));
                return InternalTree[name];
            }
        }

        #endregion

        #region OpenStreams
        /// <summary>
        /// Initializes a new instance of the FileStream class with the specified path, creation mode, read/write and sharing permission, buffer size, and synchronous or asynchronous state.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mode"></param>
        /// <param name="access"></param>
        /// <param name="share"></param>
        /// <param name="bufferSize"></param>
        /// <param name="useAsync"></param>
        /// <returns></returns>
        public FileStream OpenFileStream(
            string path, 
            System.IO.FileMode mode = FileMode.OpenOrCreate, 
            System.IO.FileAccess access = FileAccess.ReadWrite, 
            System.IO.FileShare share = FileShare.None, 
            int bufferSize = 4096, 
            bool useAsync = false)
        {
            return new FileStream(
                FilePath(path),
                mode,
                access,
                share,
                bufferSize,
                useAsync);
        }


        #endregion

        #region OpenFolders

        private DirectoryInfo _directoryInfo = null;
        /// <summary>
        /// Returns or creates new DirectoryInfo object.
        /// </summary>
        public DirectoryInfo DirectoryInfo
        {
            get => _directoryInfo ??= new DirectoryInfo(InitialDirectoryPath);
        }
        #endregion
    }
}
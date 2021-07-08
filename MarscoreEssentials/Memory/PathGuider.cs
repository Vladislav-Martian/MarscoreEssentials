using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace MarscoreEssentials.Memory
{
    public class PathGuider
    {
        #region Props
        /// <summary>
        /// Full path to folder
        /// </summary>
        public string Path { get; }
        /// <summary>
        /// Nested folders dictionary
        /// </summary>
        public Dictionary<string,PathGuider> Children { get; }
        private DirectoryInfo _directoryInfo;
        /// <summary>
        /// Simply creates DictionaryInfo instance as singleton
        /// </summary>
        public DirectoryInfo DirectoryInfo => _directoryInfo ??= new DirectoryInfo(Path);

        #endregion


        #region Structural
        /// <summary>
        /// Create guider directly by string path of folder. Class does not check path validity;
        /// </summary>
        /// <param name="path"></param>
        public PathGuider(in string path = null)
        {
            Path = path ?? Assembly.GetExecutingAssembly().Location;
            Children = new Dictionary<string, PathGuider>();
        }
        
        /// <summary>
        /// Alternative safe constructor, uses assembly location.
        /// Just create object using executing assembly of your program
        /// </summary>
        /// <param name="assembly"></param>
        /// <code>
        /// System.Reflection.Assembly.GetExecutingAssembly()
        /// </code> 
        public PathGuider(in Assembly assembly)
        {
            Path = System.IO.Path.GetDirectoryName(
                Uri.UnescapeDataString(
                    new UriBuilder(assembly.CodeBase ?? string.Empty).Path));
        }
        /// <summary>
        /// Navigation and automating building folder structure
        /// </summary>
        /// <param name="name"></param>
        public PathGuider this[string name] => CreateOne(name);

        /// <summary>
        /// Method creates one or more folders inside, and
        /// </summary>
        /// <param name="names"></param>
        public PathGuider Create(params string[] names)
        {
            foreach (var name in names)
            {
                CreateOne(name);
            }
            return this;
        }

        private PathGuider CreateOne(in string name)
        {
            if (Children.ContainsKey(name))
            {
                return Children[name];
            }
            Children[name] = new PathGuider(
                System.IO.Path.Join(
                    Path, name));
            return Children[name];
        }

        #endregion


        #region Other
        /// <summary>
        /// Generates folder structure on hard drive.
        /// </summary>
        public PathGuider Init()
        {
            LocalInit();
            foreach (var child in Children)
            {
                child.Value.Init();
            }
            return this;
        }
        private void LocalInit()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }
        #region NavigatorOptimization
        /// <summary>
        /// Return full path to some file.
        /// Argument just "contains name.extension"
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string FilePath(string filename)
        {
            return System.IO.Path.Join(Path, filename);
        }

        #endregion
        
        #region OpenStreams

        /// <summary>
        /// Initializes a new instance of the FileStream class with the specified path, creation mode, read/write and sharing permission, buffer size, and synchronous or asynchronous state.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="mode"></param>
        /// <param name="access"></param>
        /// <param name="share"></param>
        /// <param name="bufferSize"></param>
        /// <param name="useAsync"></param>
        /// <returns></returns>
        public FileStream OpenFileStream(
            string filename, 
            System.IO.FileMode mode = FileMode.OpenOrCreate, 
            System.IO.FileAccess access = FileAccess.ReadWrite, 
            System.IO.FileShare share = FileShare.None, 
            int bufferSize = 4096, 
            bool useAsync = false)
        {
            return new FileStream(
                FilePath(filename),
                mode,
                access,
                share,
                bufferSize,
                useAsync);
        }
        #endregion
        /// <summary>
        /// Removes all folders inside.
        /// </summary>
        public void DeleteAll()
        {
            foreach (var guider in Children)
            {
                Directory.Delete(guider.Value.Path);
            }
            Children.Clear();
        }
        #endregion
    }
}
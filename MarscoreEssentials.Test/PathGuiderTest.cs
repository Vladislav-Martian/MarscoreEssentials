using System;
using System.IO;
using System.Reflection;
using Marscore.Memory;
using NUnit.Framework;

namespace Marscore.Test
{
    [TestFixture]
    public class PathGuiderTest
    {
        #region Props

        public PathGuider PG;
        public PathGuider PGI;

        #endregion
        [Test, Order(1)]
        public void WayToSpecialTest()
        {
            PG = new PathGuider(Assembly.GetExecutingAssembly());
            Console.WriteLine($"Current path: {PG.Path}");
            PGI = PG["Config"]["SubConfig"].Init(PG);
            // Init() always returns own this.
            // by default starts creating directories recursively from own position
            // but you can pass any other pg object to be used as root.
            // this is done for ease of use in one line
            Assert.True(Directory.Exists(
                Path.Combine(PG.Path, "Config", "SubConfig")),
                "Directory does not created");
            Assert.True(PGI.Path == Path.Combine(PG.Path, "Config", "SubConfig"),
                $"Wrong address [{PGI.Path}]");
        }

        [Test, Order(2)]
        public void CreateFileTest()
        {
            using (var fs = PGI.OpenFileStream("test.html"))
            using (var sw = new StreamWriter(fs))
            {
                sw.WriteLine("<html></html>");
            }
            Assert.True(File.Exists(
                Path.Combine(PGI.Path, "test.html")),
                "File does not exist");
        }

        [Test, Order(3)]
        public void DirectoryDeletionTest()
        {
            Assert.True(Directory.Exists(
                Path.Combine(PG.Path, "Config", "SubConfig")),
                "Directory does not created");
            
            PG["Config"].Delete();

            Assert.False(Directory.Exists(
                PGI.Path),
                "Directory does not deleted");
        }
    }
}
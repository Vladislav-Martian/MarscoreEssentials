using System;
using System.IO;
using System.Threading;
using Marscore.Memory;
using NUnit.Framework;

namespace Marscore.Test
{
    [TestFixture]
    public class TemporalFileTest
    {
        #region Props

        

        #endregion
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        [Description("Tests file creation on default constructor, its place, its name, its deletion")]
        public void CreatingDefaultTemporalFileOnItsPlace()
        {
            var temp = new TemporalFile();
            Assert.AreEqual(temp.FolderPath + '\\', Path.GetTempPath(), "Right place:");
            Assert.False(File.Exists(temp.Path), "File not created yet:");
            temp.Open().Close();
            Assert.True(File.Exists(temp.Path), "File created and still exists:");
            Assert.True(temp.Name.Length == 40, "$0 chars-length name (UUID) + .tmp:");
            temp.Dispose(); // This must delete file
            Assert.False(File.Exists(temp.Path), "File deleted:");
            Console.WriteLine($"File created: {temp.Name}");
        }

        [Test]
        public void CreatingTemporalFileInCustomFolder()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Console.WriteLine($"Trying on:       {folder}");
            
            var p = string.Empty;
            using (var temp = new TemporalFile(dPath: folder))
            {
                p = temp.Path;
                temp.Open().Close();
                Assert.True(File.Exists(p), "File creation failed:");
                Thread.Sleep(8000);
            }
            Assert.False(File.Exists(p), "File deletion failed:");
        }
        
        [Test]
        public void CreatingTemporalFileInCustomFolderWithCustomName()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Console.WriteLine($"Trying on:       {folder}");
            
            var p = string.Empty;
            using (var temp = new TemporalFile(name: "Testing.blah", dPath: folder))
            {
                p = temp.Path;
                temp.Open().Close();
                Assert.True(File.Exists(p), "File creation failed:");
                Thread.Sleep(8000);
            }
            Assert.False(File.Exists(p), "File deletion failed:");
        }
    }
}
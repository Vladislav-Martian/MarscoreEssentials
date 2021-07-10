using System;
using System.IO;
using System.Text;
using System.Threading;
using Marscore.Memory;
using NUnit.Framework;

namespace Marscore.Test
{
    [TestFixture]
    public class TemporalFolderTest
    {
        [Test]
        public void CreatingTemporalFolderInCustomFolder()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Console.WriteLine($"Trying on:       {folder}");
            
            var p = string.Empty;
            using (var temp = new TemporalFolder(dPath: folder))
            {
                p = temp.Path;
                Assert.True(Directory.Exists(p), "Folder creation failed:");
                var file1 = temp.CreateTemporalFile("Test1.txt").Open();
                file1.Write(Encoding.UTF8.GetBytes("Testing string1"));
                file1.Close();
            }
            Assert.False(Directory.Exists(p), "Folder deletion failed:");
        }
    }
}
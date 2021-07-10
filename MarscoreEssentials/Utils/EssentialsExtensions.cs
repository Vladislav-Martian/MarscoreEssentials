using System.IO;

namespace Marscore.Essentials.Utils
{
    public static class EssentialsExtensions
    {
        public static string GetNameOnly(this FileInfo source)
        {
            return Path.GetFileNameWithoutExtension(source.FullName);
        }
    }
}
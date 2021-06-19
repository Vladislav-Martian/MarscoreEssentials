using System.IO;

namespace MarscoreEssentials.Utils
{
    public static class EssentialsExtensions
    {
        public static string GetNameOnly(this FileInfo source)
        {
            return Path.GetFileNameWithoutExtension(source.FullName);
        }
    }
}
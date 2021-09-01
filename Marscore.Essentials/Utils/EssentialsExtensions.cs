using System.IO;

namespace Marscore.Essentials.Utils
{
    public static class EssentialsExtensions
    {
        /// <summary>
        /// Returns file name without extension and path.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetNameOnly(this FileInfo source)
        {
            return Path.GetFileNameWithoutExtension(source.FullName);
        }
    }
}
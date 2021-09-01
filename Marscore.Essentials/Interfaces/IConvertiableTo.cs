namespace Marscore.Essentials.Interfaces
{
    /// <summary>
    /// Allows converting object to one type with method Convert()
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IConvertableTo<out T>
    {
        /// <summary>
        /// Converts object to type «T»
        /// </summary>
        /// <returns></returns>
        public T Convert();
    }
}
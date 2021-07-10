namespace Marscore.Essentials.Interfaces
{
    /// <summary>
    /// Generates objects via method GenerateNext().
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISource<T>
    {
        public T GenerateNext();
    }
}
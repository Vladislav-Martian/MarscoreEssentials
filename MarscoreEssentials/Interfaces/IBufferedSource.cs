namespace MarscoreEssentials.Interfaces
{
    /// <summary>
    /// Works as default ISource, but saves generated object to own property, allows to get this value without generating new.
    /// Generates objects via method GenerateNext(), but for saving value to property use GenerateSafe().
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBufferedSource<T>: ISource<T>
    {
        public T SourceBuffer { get; set; }
        public T GenerateSafe()
        {
            SourceBuffer = GenerateNext();
            return SourceBuffer;
        }
    }
}
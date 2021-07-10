namespace Marscore.Essentials.Patterns.Generative
{
    /// <summary>
    /// Simple builder pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBuilder<out T>
    {
        public abstract T Build();
    }
}
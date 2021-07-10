namespace Marscore.Essentials.Patterns.Generative
{
    /// <summary>
    /// Interface to represent object factory.
    /// <para>Factory can save configuration inside</para>
    /// <para>Creates object with method Create(), or Create(out T)</para>
    /// </summary>
    /// <typeparam name="T">Abstract Type of factory objects</typeparam>
    public interface IFactory<out T>
    {
        public abstract T Create();
    }
}
namespace MarscoreEssentials.Patterns
{
    /// <summary>
    /// Class to represent object factory.
    /// <para>Factory can save configuration inside</para>
    /// <para>Creates object with method Create(), or Create(out T)</para>
    /// </summary>
    /// <typeparam name="T">Abstract Type of factory objects</typeparam>
    public abstract class Factory<T>
    {
        public abstract T Create();
        public abstract void Create(out T variable);
    }
}
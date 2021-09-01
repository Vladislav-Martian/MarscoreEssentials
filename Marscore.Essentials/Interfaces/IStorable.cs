using System;

namespace Marscore.Essentials.Interfaces
{
    /// <summary>
    /// Interface aalows to store any object as another object, and restore it back.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStorable<T>
    {
        public T Store();
        public void Store(out T variable) => variable = Store();
        public IStorable<T> Restore(in T source);
        public void Restore(out IStorable<T> variable, in T source) => variable = Restore(source);
    }

    /// <summary>
    /// Default variation of IStorable«T» where T is byte[].
    /// Store object to byte array and restore it back.
    /// </summary>
    public interface IStorable : IStorable<byte[]>
    {
    }
    
    /// <summary>
    /// Default variation of IStorable«T» where T is string.
    /// Store object to string and restore it back.
    /// </summary>
    public interface IStringStorable : IStorable<string>
    {
    }
}
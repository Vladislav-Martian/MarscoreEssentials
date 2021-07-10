using System;
using System.Collections.Generic;

namespace Marscore.Essentials.Interfaces
{
    /// <summary>
    /// For crating tree-like elements, like xml.
    /// </summary>
    public interface ITreeElement: IList<ITreeElement>
    {
        /// <summary>
        /// Returns tree parent element, like
        /// </summary>
        public ITreeElement Parent { get; }
        /// <summary>
        /// Returns list of tree elements, side by side to this
        /// </summary>
        public IList<ITreeElement> Siblings
        {
            get => Parent.Children;
        }
        /// <summary>
        /// Returns list of tree elements, inside this
        /// </summary>
        public IList<ITreeElement> Children { get; }
        /// <summary>
        /// Leaves can`t contain elements inside.
        /// </summary>
        public bool IsLeaves { get; }
        /// <summary>
        /// Indexer returns inner element by index, or throws an exception;
        /// </summary>
        /// <exception cref="FieldAccessException"></exception>
        /// <param name="index"></param>
        public new ITreeElement this[int index]
        {
            get
            {
                return IsLeaves ? throw new FieldAccessException("Element is a leaf, doesn't contains elements inside") 
                    : Children[index];
            }
            set
            {
                if (IsLeaves)
                {
                    throw new FieldAccessException("Element is a leaf, doesn't contains elements inside");
                }
                if (value is ITreeElement v)
                {
                    Children[index] = v;
                }
            }
        }
        /// <summary>
        /// Returns by index item on same nesting level.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ITreeElement GetSibling(int index)
        {
            return Siblings[index];
        }
        /// <summary>
        /// Returns by index item on next nesting level.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ITreeElement GetChild(int index)
        {
            return Children[index];
        }
        /// <summary>
        /// Returns just next item on same nesting level.
        /// </summary>
        /// <returns></returns>
        public ITreeElement GetNextSibling()
        {
            return Siblings[Siblings.IndexOf(this) + 1];
        }
        /// <summary>
        /// Returns item from next nesting level by index 0.
        /// </summary>
        /// <returns></returns>
        public ITreeElement GetFirstChild()
        {
            return Children[0];
        }
    }
}
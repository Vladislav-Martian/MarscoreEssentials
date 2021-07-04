using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MarscoreEssentials.UnusualData
{
    public sealed class Words: IList<Word>
    {
        #region Props

        public List<Word> ListWords { get; set; }

        #endregion

        #region Structors

        public Words(params string[] words)
        {
            ListWords = new List<Word>();
            foreach (var word in words)
            {
                ListWords.Add(new Word(word));
            }
        }
        
        public Words(IEnumerable<string> words)
        {
            ListWords = new List<Word>();
            foreach (var word in words)
            {
                ListWords.Add(new Word(word));
            }
        }
        
        public Words(params Word[] words)
        {
            ListWords = new List<Word>();
            foreach (var word in words)
            {
                ListWords.Add(word);
            }
        }
        
        public Words(IEnumerable<Word> words)
        {
            ListWords = new List<Word>();
            foreach (var word in words)
            {
                ListWords.Add(word);
            }
        }

        #endregion
        public IEnumerator<Word> GetEnumerator()
        {
            return ListWords.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ListWords.GetEnumerator();
        }

        public void Add(Word item)
        {
            ListWords.Add(item);
        }
        
        public void Add(string item)
        {
            ListWords.Add(new Word(item));
        }

        public void Clear()
        {
            ListWords.Clear();
        }

        public bool Contains(Word item)
        {
            return ListWords.Contains(item);
        }

        public void CopyTo(Word[] array, int arrayIndex)
        {
            ListWords.CopyTo(array, arrayIndex);
        }

        public bool Remove(Word item)
        {
            return ListWords.Remove(item);
        }

        public int Count
        {
            get => ListWords.Count;
        }

        public bool IsReadOnly
        {
            get => false;
        }
        public int IndexOf(Word item)
        {
            return ListWords.IndexOf(item);
        }

        public void Insert(int index, Word item)
        {
            ListWords.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ListWords.RemoveAt(index);
        }

        public Word this[int index]
        {
            get => ListWords[index];
            set => ListWords[index] = value;
        }

        public string ToString(in char separator = '.')
        {
            return string.Join(separator, ListWords);
        }

        public static implicit operator string(in Words words) => words.ToString();

    }
}
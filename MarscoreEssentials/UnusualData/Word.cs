using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MarscoreEssentials.Exceptions;
using MarscoreEssentials.Interfaces;

namespace MarscoreEssentials.UnusualData
{
    public sealed class Word: ICloneable, IComparable<Word>, IComparable, IReadOnlyCollection<char>, IStorable<string>
    {
        /// <summary>
        /// Just simple regular expression to test any string to being right Word.
        /// </summary>
        public static readonly Regex WordRegex = new Regex(@"^[a-zA-Z][a-zA-Z0-9_]*$");
        /// <summary>
        /// String with word.
        /// </summary>
        public string Text { get; private set; }
        
        /// <summary>
        /// Tests string and generates Word object
        /// </summary>
        /// <param name="origin"></param>
        /// <exception cref="LexingException"></exception>
        public Word(in string origin)
        {
            var word = origin.Trim();
            if (WordRegex.IsMatch(word))
            {
                Text = word;
            }
            throw new LexingException($"String '{word}' doesn't matches regex.");
        }
        
        /// <summary>
        /// private fast constructor, creating object without testing word.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="ignoreIt"></param>
        private Word(in string origin, int ignoreIt)
        {
            Text = origin;
        }

        public static implicit operator string(in Word word) => word.Text;
        public static implicit operator Word(in string word) => new Word(word);

        public object Clone()
        {
            return new Word(Text, 0);
        }

        public int CompareTo(Word other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(Text, other.Text, StringComparison.Ordinal);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Word other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Word)}");
        }

        public IEnumerator<char> GetEnumerator()
        {
            return Text.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get => Text.Length;
        }
        public int Length
        {
            get => Text.Length;
        }

        public override bool Equals(object? obj)
        {
            return Text.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        public override string ToString()
        {
            return Text;
        }
        
        public static Word operator +(in Word w1, in Word w2)
        {
            return new Word(w1.Text + "_" + w2.Text, 0);
        }

        public char this[int index]
        {
            get => Text[index];
        }

        public string Store()
        {
            return ToString();
        }

        public IStorable<string> Restore(in string source)
        {
            var word = source.Trim();
            if (!WordRegex.IsMatch(word))
                throw new LexingException($"String '{word}' doesn't matches regex.");
            Text = word;
            return this;
        }
    }
}
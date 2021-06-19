using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace MarscoreEssentials.Exceptions
{
    [Serializable]
    public class LexingException : MSException
    {

        public LexingException()
        {
        }

        public LexingException(string message) : base(message)
        {
        }

        public LexingException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LexingException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
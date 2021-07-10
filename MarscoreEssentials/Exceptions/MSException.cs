using System;
using System.Runtime.Serialization;

namespace Marscore.Essentials.Exceptions
{
    [Serializable]
    public class MSException : Exception
    {

        public MSException()
        {
        }

        public MSException(string message) : base(message)
        {
        }

        public MSException(string message, Exception inner) : base(message, inner)
        {
        }

        protected MSException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
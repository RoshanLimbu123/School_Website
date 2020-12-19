using System;
using System.Runtime.Serialization;

namespace CMS.Core.Dto
{
    [Serializable]
    internal class NonEmptyException : Exception
    {
        public NonEmptyException()
        {
        }

        public NonEmptyException(string message) : base(message)
        {
        }

        public NonEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NonEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
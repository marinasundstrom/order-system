using System;
using System.Runtime.Serialization;

namespace Commerce.Domain.Exceptions
{
    [Serializable]
    internal class InvalidSsnException : Exception
    {
        public InvalidSsnException()
        {
        }

        public InvalidSsnException(string? message) : base(message)
        {
        }

        public InvalidSsnException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidSsnException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System.Runtime.Serialization;

namespace QuizzApp.Exceptions
{
    [Serializable]
    internal class UnableToFetchException : Exception
    {
        public UnableToFetchException()
        {
        }

        public UnableToFetchException(string? message) : base(message)
        {
        }

        public UnableToFetchException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToFetchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
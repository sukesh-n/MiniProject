using System.Runtime.Serialization;

namespace QuizzApp.Exceptions
{
    [Serializable]
    public class EmptyDatabaseException : Exception
    {
        public EmptyDatabaseException()
        {
        }

        public EmptyDatabaseException(string? message) : base(message)
        {
        }

        public EmptyDatabaseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmptyDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
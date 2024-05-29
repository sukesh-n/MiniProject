using System.Runtime.Serialization;

namespace QuizzApp.Exceptions
{
    [Serializable]
    internal class EmptyRepositoryException : Exception
    {
        public EmptyRepositoryException()
        {
        }

        public EmptyRepositoryException(string? message) : base(message)
        {
        }

        public EmptyRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmptyRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
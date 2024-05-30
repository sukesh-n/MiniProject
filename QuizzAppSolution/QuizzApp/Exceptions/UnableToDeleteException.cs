using System.Runtime.Serialization;

namespace QuizzApp.Exceptions
{
    [Serializable]
    internal class UnableToDeleteException : Exception
    {
        private Exception ex;

        public UnableToDeleteException()
        {
        }

        public UnableToDeleteException(Exception ex)
        {
            this.ex = ex;
        }

        public UnableToDeleteException(string? message) : base(message)
        {
        }

        public UnableToDeleteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
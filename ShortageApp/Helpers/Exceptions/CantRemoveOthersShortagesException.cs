using System.Runtime.Serialization;

namespace ShortageApp.Helpers.Exceptions
{
    [Serializable]
    public class CantRemoveOthersShortagesException : Exception
    {
        public CantRemoveOthersShortagesException()
        {
        }

        public CantRemoveOthersShortagesException(string? message) : base(message)
        {
        }

        public CantRemoveOthersShortagesException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CantRemoveOthersShortagesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

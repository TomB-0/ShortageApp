using System.Runtime.Serialization;

namespace ShortageApp.Helpers.Exceptions
{
    [Serializable]
    public class ShortageDoesntExistException : Exception
    {
        public ShortageDoesntExistException()
        {
        }

        public ShortageDoesntExistException(string? message) : base(message)
        {
        }

        public ShortageDoesntExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ShortageDoesntExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

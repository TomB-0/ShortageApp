using System.Runtime.Serialization;

namespace ShortageApp.Helpers.Exceptions
{
    [Serializable]
    public class ShortageAlreadyExistsException : Exception
    {
        public ShortageAlreadyExistsException()
        {
        }

        public ShortageAlreadyExistsException(string? message) : base(message)
        {
        }

        public ShortageAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ShortageAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

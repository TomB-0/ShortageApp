using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShortageApp.Helpers.Exceptions
{
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

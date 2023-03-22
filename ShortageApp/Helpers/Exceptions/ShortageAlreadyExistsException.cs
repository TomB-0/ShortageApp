using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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

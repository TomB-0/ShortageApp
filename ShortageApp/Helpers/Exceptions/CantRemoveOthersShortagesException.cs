using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShortageApp.Helpers.Exceptions
{
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public enum Severity { ERROR, WARN, INFO, DEBUG }
        protected Severity severity;

        public BaseException() { }


        public BaseException(string message) : base(message) { }

        public BaseException(string message, Severity severity)
            : base(message)
        {
            this.severity = severity;
        }

        public BaseException(string message, Exception inner) : base(message, inner) { }

        public BaseException(string message, Exception inner, Severity severity)
            : base(message, inner)
        {
            this.severity = severity;
        }

        public BaseException(System.Runtime.Serialization.SerializationInfo info,
                             System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    } // end BaseException class
} // end namespace

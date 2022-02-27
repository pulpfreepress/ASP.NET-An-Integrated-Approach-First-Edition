using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Exceptions
{
    [Serializable]
    public class DBException : BaseException
    {
        public DBException() { }


        public DBException(string message) : base(message) { }

        public DBException(string message, Severity severity)
            : base(message, severity) { }

        public DBException(string message, Exception inner) : base(message, inner) { }

        public DBException(string message, Exception inner, Severity severity)
            : base(message, inner, severity) { }

        public DBException(System.Runtime.Serialization.SerializationInfo info,
                             System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    } // end DBException class
} // end namespace


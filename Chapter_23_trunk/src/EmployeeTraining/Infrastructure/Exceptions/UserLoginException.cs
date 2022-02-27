using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Exceptions {

    [Serializable]
    public class UserLoginException : BaseException {

        public UserLoginException() { }


        public UserLoginException(string message) : base(message) { }

        public UserLoginException(string message, Severity severity)
            : base(message, severity) { }

        public UserLoginException(string message, Exception inner) : base(message, inner) { }

        public UserLoginException(string message, Exception inner, Severity severity)
            : base(message, inner, severity) { }

        public UserLoginException(System.Runtime.Serialization.SerializationInfo info,
                               System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    } // end UserLoginException class definition
} // end namespace


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Exceptions {

    [Serializable]
    public class UserAuthorizationException : BaseException {

        public UserAuthorizationException() { }


        public UserAuthorizationException(string message) : base(message) { }

        public UserAuthorizationException(string message, Severity severity)
            : base(message, severity) { }

        public UserAuthorizationException(string message, Exception inner) : base(message, inner) { }

        public UserAuthorizationException(string message, Exception inner, Severity severity)
            : base(message, inner, severity) { }

        public UserAuthorizationException(System.Runtime.Serialization.SerializationInfo info,
                               System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    } // end UserAuthorizationException class definition
} // end namespace



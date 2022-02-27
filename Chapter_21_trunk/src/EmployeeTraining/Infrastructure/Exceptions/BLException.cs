using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Exceptions {

    [Serializable]
    public class BLException : BaseException {

      public BLException() { }


      public BLException(string message) : base(message) { }

      public BLException(string message, Severity severity)
            : base(message, severity) { }

      public BLException(string message, Exception inner) : base(message, inner) { }

      public BLException(string message, Exception inner, Severity severity)
            : base(message, inner, severity) { }

      public BLException(System.Runtime.Serialization.SerializationInfo info,
                             System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    } // end BLException class definition
} // end namespace

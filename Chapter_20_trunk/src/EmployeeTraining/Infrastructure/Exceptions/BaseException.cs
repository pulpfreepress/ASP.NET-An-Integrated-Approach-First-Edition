using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Exceptions
{
    /// <summary>
    /// BaseException class serves as the base class for all application exceptions.
    /// </summary>
    [Serializable]
    public class BaseException : Exception
    {

        /// <summary>
        /// Severity enum indicates the severity of the exception.
        /// </summary>
        public enum Severity { ERROR, WARN, INFO, DEBUG }
        /// <summary>
        /// Severity field used to maintain exception severity
        /// </summary>
        protected Severity severity;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseException() { }


        /// <summary>
        /// Constructor with string argument
        /// </summary>
        /// <param name="message">Represents nature of exception.</param>
        public BaseException(string message) : base(message) { }

        /// <summary>
        /// Constructor with string and severity params.
        /// </summary>
        /// <param name="message">Nature of exception</param>
        /// <param name="severity">Severity of exception.</param>
        public BaseException(string message, Severity severity)
            : base(message)
        {
            this.severity = severity;
        }

        /// <summary>
        /// Constructor with string and inner exception parameters.
        /// </summary>
        /// <param name="message">nature of exception</param>
        /// <param name="inner">Exception that caused exception</param>
        public BaseException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Constructor with string, inner exception, and severity params.
        /// </summary>
        /// <param name="message">Nature of exception</param>
        /// <param name="inner">Exception that caused exception</param>
        /// <param name="severity">Severity of exception</param>
        public BaseException(string message, Exception inner, Severity severity)
            : base(message, inner)
        {
            this.severity = severity;
        }

        /// <summary>
        /// Constructor with info and context params
        /// </summary>
        /// <param name="info">Information about the exception</param>
        /// <param name="context">Exception context</param>
        public BaseException(System.Runtime.Serialization.SerializationInfo info,
                             System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    } // end BaseException class
} // end namespace

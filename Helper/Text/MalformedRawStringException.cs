using System;

namespace Helper.Text
{
    /// <summary>
    /// An exception that will be thrown while a string being parsed in a <see cref="FixedLengthFieldString"/> object
    /// is malformed.
    /// </summary>
    public class MalformedRawStringException : Exception
    {
        /// <summary>
        /// The malformed raw string.
        /// </summary>
        public string RawString { get; private set; }

        /// <summary>
        /// Initializes the <see cref="MalformedRawStringException"/> without specifying the malformed raw string.
        /// </summary>
        public MalformedRawStringException()
        {
        }

        /// <summary>
        /// Initializes the <see cref="MalformedRawStringException"/> with a given exception message, but not
        /// specifying the malformed raw string.
        /// </summary>
        public MalformedRawStringException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes the <see cref="MalformedRawStringException"/> with a given exception message and a malformed
        /// raw string.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="rawString">The malformed raw string.</param>
        public MalformedRawStringException(string message, string rawString)
            : base(message)
        {
            this.RawString = rawString;
        }

        /// <summary>
        /// Initializes the <see cref="MalformedRawStringException"/> with a given exception message and an inner-
        /// exception, but no malformed raw string.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">An inner-exception.</param>
        public MalformedRawStringException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes the <see cref="MalformedRawStringException"/> with a given exception message, an inner-
        /// exception, and a malformed raw string.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="rawString">The malformed raw string.</param>
        /// <param name="innerException">An inner-exception.</param>
        public MalformedRawStringException(string message, string rawString, Exception innerException)
            : base(message, innerException)
        {
            this.RawString = rawString;
        }
    }
}
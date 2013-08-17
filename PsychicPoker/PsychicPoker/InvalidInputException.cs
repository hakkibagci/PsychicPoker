using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PsychicPoker
{
    /// <summary>
    /// This type of exception is used for notification of invalid inputs.
    /// </summary>
    [Serializable()]
    public class InvalidInputException : System.Exception
    {
        public InvalidInputException() : base() { }

        public InvalidInputException(string message) : base(message) { }

        public InvalidInputException(string message, System.Exception inner) : base(message, inner) { }

        protected InvalidInputException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}

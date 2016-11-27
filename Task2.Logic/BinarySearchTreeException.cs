using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    /// <summary>
    /// Exceptions for BinarySearch tree
    /// </summary>
    public class BinarySearchTreeException : Exception
    {
        public BinarySearchTreeException() { }
        public BinarySearchTreeException(string message)
            : base(message) { }
        public BinarySearchTreeException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        public BinarySearchTreeException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}

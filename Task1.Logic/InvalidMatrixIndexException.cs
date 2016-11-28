using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    /// <summary>
    /// Provides exceptions for matrix operations
    /// </summary>
    public class InvalidMatrixIndexException : Exception
    {
        public InvalidMatrixIndexException()
        {
        }

        public InvalidMatrixIndexException(string message) : base(message)
        {
        }

        public InvalidMatrixIndexException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidMatrixIndexException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

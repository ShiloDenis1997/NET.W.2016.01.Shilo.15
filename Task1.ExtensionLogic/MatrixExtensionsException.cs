using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task1.ExtensionLogic
{
    /// <summary>
    /// Provides exception for matrix extensions
    /// </summary>
    public class MatrixExtensionsException : Exception
    {
        public MatrixExtensionsException()
        {
        }

        public MatrixExtensionsException(string message) : base(message)
        {
        }

        public MatrixExtensionsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MatrixExtensionsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League.BL.Exceptions
{
    public class SpelerManagerException : Exception
    {
        public SpelerManagerException(string? message) : base(message)
        {
        }

        public SpelerManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

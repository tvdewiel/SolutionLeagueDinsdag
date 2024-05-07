using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League.DL.Exceptions
{
    public class ADORepositoryException : Exception
    {
        public ADORepositoryException(string? message) : base(message)
        {
        }

        public ADORepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

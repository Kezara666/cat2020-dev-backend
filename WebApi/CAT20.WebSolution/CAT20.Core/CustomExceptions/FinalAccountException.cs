using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.CustomExceptions
{
    public class FinalAccountException : Exception
    {
        public FinalAccountException() : base() { }
        public FinalAccountException(string message) : base(message) { }
        public FinalAccountException(string message, Exception innerException) : base(message, innerException) { }
    }
}

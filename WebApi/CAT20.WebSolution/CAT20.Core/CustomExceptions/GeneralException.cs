using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.CustomExceptions
{
    public class GeneralException : Exception
    {
        public GeneralException() : base() { }
        public GeneralException(string message) : base(message) { }
        public GeneralException(string message, Exception innerException) : base(message, innerException) { }
    }
}

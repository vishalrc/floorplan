using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Security.TokenAuth
{
    [Serializable]
    public class SecurityTokenException : System.Exception
    {
        public SecurityTokenException() { }

        public SecurityTokenException(string message)
            : base(message) { }

        public SecurityTokenException(string message, System.Exception inner)
            : base(message, inner) { }

        public SecurityTokenException(string format, System.Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}

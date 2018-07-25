using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Common.Utility
{
    public class APIResponseBody : IDisposable
    {
        public string type { get; set; }
        public string body { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public int subcode { get; set; }

        public void Dispose() { }
    }

    public class APIClientResponseBody : IDisposable
    {
        public string Message { get; set; }
        public void Dispose() { }
    }
}

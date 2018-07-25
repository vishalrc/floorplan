using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.JLT.Entity
{
    public class Role : IDisposable
    {
        public int? id { get; set; }
        public string rolename { get; set; }
        public string description { get; set; }
        public UInt64? rolevalue { get; set; }
        public bool? isactive { get; set; }

        public void Dispose(){}
    }
}

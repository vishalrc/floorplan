using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.JLT.Entity
{
    public class Config : IDisposable
    {
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }

        public void Dispose() { }
    }
}

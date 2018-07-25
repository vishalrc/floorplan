using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Floorplan.Entity
{
    public class Config : IDisposable
    {
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }

        public void Dispose() { }
    }
}

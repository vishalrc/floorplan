using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Floorplan.Entity
{
    public class Country : IDisposable
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public bool? isactive { get; set; }

        public void Dispose() { }
    }
}

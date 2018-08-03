using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Floorplan.Entity
{
    public class building
    {
        public int? buildingid { get; set; }
        public string buildingcode { get; set; }
        public string buildingname { get; set; }
        public bool isactive { get; set; }
    }
}

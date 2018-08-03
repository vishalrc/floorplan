using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Floorplan.Entity
{
    public class floor
    {
        public int? floorid { get; set; }
        public string floorcode { get; set; }
        public string floorname { get; set; }

        public int buildingid { get; set; }
        public bool isactive { get; set; }
    }
}

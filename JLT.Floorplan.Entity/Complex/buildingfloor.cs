using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLT.Floorplan.Entity
{
    public class buildingfloor
    {
        public int? floorid { get; set; }
        public string  floorname { get; set; }
        public string buildingname { get; set; }
        public string buildingcode { get; set; }
        public bool isactive { get; set; }
    }
}


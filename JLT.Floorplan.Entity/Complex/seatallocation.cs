using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Floorplan.Entity
{
  public  class seatallocation
    {
        public int? allocationid { get; set; }
        public string employeeno { get; set; }
        public int seatno { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public int days { get; set; }
        public bool isactive { get; set; }
    }
}

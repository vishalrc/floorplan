using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Floorplan.Entity
{
  public  class seatallocation
    {
        public int allocationid { get; set; }
        public Employee employee { get; set; }
        public seat seat { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int days { get; set; }
        public bool isactive { get; set; }
    }
}

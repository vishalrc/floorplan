﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Floorplan.Entity
{
    public class seat
    {
        public int? seatid { get; set; }
        public string seatlabel { get; set; }
        public string cubicalno { get; set; }
        public int rowno { get; set; }
        public int columnno { get; set; }
        public int colspan { get; set; }
        public int rowspan { get; set; }
        public int teamid { get; set; }
        public int floorid { get; set; }
        public string type { get; set; }
        public bool isbooked { get; set; }

        public string associateno { get; set; }
        public string name { get; set; }
    }
}

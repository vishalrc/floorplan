using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.JLT.Entity
{
    public class seat
    {
        public int seatid { get; set; }
        public string seatlabel { get; set; }
        public string cubicalno { get; set; }
        public int rowno { get; set; }
        public int columnno { get; set; }
        public int teamid { get; set; }
        public int floorid { get; set; }
        public string type { get; set; }
    }
}

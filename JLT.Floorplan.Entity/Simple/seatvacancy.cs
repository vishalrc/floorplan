using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.JLT.Entity
{
    public class seatvacancy
    {
        public int seatvacancyid     { get; set; }
        public int associateno { get; set; }
        public DateTime startdate { get; set; }

        public DateTime enddate { get; set; }

        public string vacancytype { get; set; }
    }
    
}

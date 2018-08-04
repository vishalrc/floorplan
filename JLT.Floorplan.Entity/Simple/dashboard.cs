using System;

namespace JLT.Floorplan.Entity
{
    public class dashboard : IDisposable
    {
        public int totalseat { get; set; }
        public int occupiedseat { get; set; }
        public int vacantseat { get; set; }
        public int seatavailablemorethan10days { get; set; }
        public int seatavailablemorethan5days { get; set; }
        public int seatavailablelessthan5days { get; set; }
        public void Dispose() { }
    }
}

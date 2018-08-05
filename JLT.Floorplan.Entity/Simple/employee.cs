using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Floorplan.Entity
{
    public class Employee : IDisposable
    {
        public UInt64? associateid { get; set; }
        public string associateno { get; set; }
        public string name { get; set; }
        public string dob { get; set; }
        public string emailid { get; set; }
        public string phoneno { get; set; }
        public string profile { get; set; }
        public string ut1 { get; set; }
        public string ut2 { get; set; }
        public string ut3 { get; set; }
        public string ut4 { get; set; }
        public string ut5 { get; set; }
        public string departmentid { get; set; }
        public string departmentname { get; set; }
        public byte[] photo { get; set; }
        public bool? isactive { get; set; }
        public string seatno{ get; set; }
        public bool onleave { get; set; }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

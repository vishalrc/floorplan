using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Floorplan.Entity
{
    public class CurrentLogedInUser : IDisposable
    {
        public UInt64 id { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
        public int usertypeid { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string emailid { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string profilepic { get; set; }

        public string authtoken { get; set; }
        public UInt64 role { get; set; }

        public string lastlogindate { get; set; }
        public string lastpasswordchangedate { get; set; }
        public string lastfailedattemptdate { get; set; }

        public string assigntests { get; set; }

        public CurrentLogedInUser() { }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Floorplan.Entity
{
    public class user : IDisposable
    {
        public user() { }

        public UInt64? id { get; set; }
        public string userid { get; set; }
        public int? usertypeid { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string emailid { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string profilepic { get; set; }
        public int? country { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public int? isenabled { get; set; }
        public string code { get; set; }
        public string hash { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public UInt64? addedby { get; set; }
        public UInt64? editedby { get; set; }
        public string timestamp { get; set; }
        public bool? islockedout { get; set; }
        public string lastlogindate { get; set; }
        public string lastpasswordchangedate { get; set; }
        public string lastfailedattemptdate { get; set; }
        public int? failedpasswordattemptcount { get; set; }

        public string roles { get; set; }
        public string encrykey { get; set; }


        public UInt64 role { get; set; }
        public string authtoken { get; set; }

        public string assigntests { get; set; }

        public void Dispose() { }
    }
}

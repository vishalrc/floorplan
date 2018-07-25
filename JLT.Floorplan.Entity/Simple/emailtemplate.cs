using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.JLT.Entity
{
    public class emailtemplate: IDisposable
    {
        public UInt64 id { get; set; }
        public UInt64 templateid { get; set; }
        public UInt64 accountid { get; set; }
        public string name { get; set; }
        public string subject { get; set; }
        public string emailbody { get; set; }
        public string bookmarkpre { get; set; }
        public string bookmarksuf { get; set; }
        public string emailto { get; set; }
        public string emailfrom { get; set; }
        public string emailbcc { get; set; }
        public string emailcc { get; set; }
        public string viewname { get; set; }
        public bool enabled { get; set; }
        public bool featured { get; set; }
        public UInt64? editedby { get; set; }

        public void Dispose() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.JLT.Common.Utility
{
    public static class Enums
    {
        public enum JsonMsgType
        {
            Success = 1,
            Warning = 2,
            Info = 3,
            Error = 4
        }

        [Flags]
        public enum Action
        {
            General = 1,
            Add_Edit_User = 2,
            Delete_Archive_User = 4,
            Add_Edit_Test = 8,
            Authorizing = 16,
            Proctoring = 32
        }

        public enum UserType
        {
            Admin = 1,
            Proctor = 2,
            Authorizer = 3,
            Examinee = 4,
            Common = 5
        }

        public enum UserRoles
        {
            General = 1,
            Admin = 15,
            Authorizer = 17,
            Proctor = 33
        }

        public enum Severity
        {
            Low = 0,
            Medium = 1,
            High = 2,
            Critical = 3
        }
    }
}

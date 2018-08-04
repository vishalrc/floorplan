using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLT.Common.Utility
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
            Add_Edit_Employee = 8,
        }

        public enum UserType
        {
            Admin = 1,
            Manager = 2,
            Teammember = 3,
            Common = 5
        }

        public enum UserRoles
        {
            General = 1,
            Admin = 15,
            Manager = 17,
            TeamMember = 33
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

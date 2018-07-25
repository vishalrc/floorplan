using System;
using System.Security.Principal;
using com.JLT.Entity;

namespace com.JLT.Common.Utility
{
    public class AuthenticationWebPlatformPrincipal : IPrincipal
    {
        private IIdentity identity;
        private CurrentLogedInUser userData;

        public AuthenticationWebPlatformPrincipal(IIdentity identity, CurrentLogedInUser udata)
        {
            this.identity = identity;
            this.userData = udata;
        }

        public CurrentLogedInUser UserData
        {
            get
            {
                return userData;
            }
        }

        #region IPrincipal Members

        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(string role)
        {
            if ((Convert.ToInt64(userData.role) & Convert.ToInt64(role)) == (Convert.ToInt64(userData.role)))
                return true;
            else
                return false;
        }

        #endregion
    }
}

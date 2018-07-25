using System;
using System.Security.Principal;
using JLT.Floorplan.Entity;

namespace JLT.Common.Utility
{
    public class AuthenticationWebPlatformPrincipal : IPrincipal
    {
        private CurrentLogedInUser userData;

        public AuthenticationWebPlatformPrincipal(IIdentity identity, CurrentLogedInUser udata)
        {
            this.Identity = identity;
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

        public IIdentity Identity { get; }

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

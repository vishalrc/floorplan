using System.Web;
using System.Web.Mvc;
using JLT.Common.Utility;

namespace JLT.Floorplan.RestAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorLoggerFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
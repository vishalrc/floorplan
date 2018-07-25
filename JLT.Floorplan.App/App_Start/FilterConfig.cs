using System.Web;
using System.Web.Mvc;
using com.JLT.Common.Utility;

namespace com.JLT.eProctor
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
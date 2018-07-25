using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace com.JLT.RestAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
               name: "DefaultCustomApi",
               routeTemplate: "api/{controller}/{action}",
               defaults: new { controller = "values", action = "getall" }
           );
            config.Routes.MapHttpRoute(
               name: "DefaultIdCustomApi",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { controller = "values", action = "getall", id = RouteParameter.Optional },
               constraints: new { id = @"^[0-9]+$" }
           );
            config.Routes.MapHttpRoute(
               name: "DefaultValueCustomApi",
               routeTemplate: "api/{controller}/{action}/{value}",
               defaults: new { controller = "values", action = "getall", value = RouteParameter.Optional }
           );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FinkiOverflowProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "get", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "StudentsApi",
                routeTemplate: "api/{controller}/{action}/{studentId}/{id}",
                defaults: new { action = "get"}
            );
        }
    }
}

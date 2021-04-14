using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebAPIRestaurant
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}/{opt}",
                defaults: new { id = RouteParameter.Optional, opt = RouteParameter.Optional }
            );
        }
    }
}

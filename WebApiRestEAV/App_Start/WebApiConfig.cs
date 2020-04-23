using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebApiRestEAV
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Tipos de formatos
            //config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("frmt", "json", new MediaTypeHeaderValue("application/json")));
            //config.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("frmt","xml",new MediaTypeHeaderValue("application/xml")));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApiRestEAV
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            HttpConfiguration httpconfig = GlobalConfiguration.Configuration;
            httpconfig.Formatters.Remove(httpconfig.Formatters.XmlFormatter);
        }
    }
}

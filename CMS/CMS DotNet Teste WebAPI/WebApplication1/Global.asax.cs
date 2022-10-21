using Newtonsoft.Json;
using System.Globalization;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApplication1
{

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //System.Data.Entity.Database.SetInitializer(new DatabaseInitializer());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                Culture = CultureInfo.GetCultureInfo("pt-br"),
                DateFormatString = "dd/MM/yyyy",
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}

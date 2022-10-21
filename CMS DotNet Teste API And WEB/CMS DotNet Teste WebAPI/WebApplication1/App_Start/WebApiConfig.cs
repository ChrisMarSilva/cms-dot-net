using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using WebApplication1.Filters;

namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
                       
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;

            jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None; 
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // config.Formatters.XmlFormatter.SupportedMediaTypes.Clear(); //By default Web API return XML data  //We can remove this by clearing the SupportedMediaTypes option as follows  
            settings.Formatting = Formatting.Indented;
            // config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented; //Now set the serializer setting for JsonFormatter to Indented to get Json Formatted data  
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); //For converting data in Camel Case  
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(name: "Aulas",      routeTemplate: "api/curso/{idCurso}/aulas", defaults: new { controller = "Aulas" });
            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}",     defaults: new { id = RouteParameter.Optional });

            // Web API filters
            config.Filters.Add(new ValidationExceptionFilterAttribute());
        }
    }
}

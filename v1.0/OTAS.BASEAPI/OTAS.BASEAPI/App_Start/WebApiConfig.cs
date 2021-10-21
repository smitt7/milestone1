using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OTAS.BASEAPI
{
    public static class WebApiConfig
    {
        private static object jsonSerializerSettings;

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //Cross Domain
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            //Force JSON responses on all requests
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
            GlobalConfiguration.Configuration.Formatters.Add(new XmlMediaTypeFormatter());

            //var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("xml", "true", "application/xml"));

            //Setup to return json and camelcase it!
            SerializeSettings(GlobalConfiguration.Configuration);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        static void SerializeSettings(HttpConfiguration config)
        {
            var jsonSetting = jsonSerializer;
            config.Formatters.JsonFormatter.SerializerSettings = jsonSetting;
        }
        public static JsonSerializerSettings jsonSerializer
        {
            get
            {
                var jsonSetting = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                    //,Formatting = Formatting.Indented                 
                };
                jsonSetting.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                return jsonSetting;
            }
        }

    }   
}


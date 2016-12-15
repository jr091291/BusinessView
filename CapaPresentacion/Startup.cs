using CapaLogica.Identity.Infraestructure;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(CapaPresentacion.Startup))]
namespace CapaPresentacion
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);

            ConfigureWebApi(httpConfig);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            EnableSwagger(httpConfig);

            app.UseWebApi(httpConfig);
        }

        private void EnableSwagger(HttpConfiguration httpConfig)
        {
            httpConfig
                .EnableSwagger(c => c.SingleApiVersion("v1", "BusinessView 1.0"))
                .EnableSwaggerUi();
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            StartupOwingConfig.RegisterOwinContextManager(app);

            // Plugin the OAuth bearer JSON Web Token tokens generation and Consumption will be here

        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
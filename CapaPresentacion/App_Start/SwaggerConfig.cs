using System.Web.Http;
using WebActivatorEx;
using CapaPresentacion;
using Swashbuckle.Application;
using System.Net.Http;
using System;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace CapaPresentacion
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
            .EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Almacen services");
                c.RootUrl(req =>
                   req.RequestUri.GetLeftPart(UriPartial.Authority) +
                   req.GetRequestContext().VirtualPathRoot.TrimEnd('/'));
            }).EnableSwaggerUi() ;
            

            //        .EnableSwagger(c =>
            //            {
            //            By default, the service root url is inferred from the request used to access the docs.
            //                 However, there may be situations (e.g.proxy and load - balanced environments) where this does not
            //                 resolve correctly. You can workaround this by providing your own code to determine the root URL.


            //                c.RootUrl(req => GetRootUrlFromAppConfig());

            //    If schemes are not explicitly provided in a Swagger 2.0 document, then the scheme used to access
            //                 the docs is taken as the default.If your API supports multiple schemes and you want to be explicit

            //                about them, you can use the "Schemes" option as shown below.


            //               c.Schemes(new[] { "http", "https" });

            //                 Use "SingleApiVersion" to describe a single version API.Swagger 2.0 includes an "Info" object to
            //                 hold additional metadata for an API. Version and title are required but you can also provide
            //                 additional fields by chaining methods off SingleApiVersion.

            //                c.SingleApiVersion("v1", "CapaPresentacion");

            //If your API has multiple versions, use "MultipleApiVersions" instead of "SingleApiVersion".
            //                 In this case, you must provide a lambda that tells Swashbuckle which actions should be
            //                 included in the docs for a given API version. Like "SingleApiVersion", each call to "Version"
            //                 returns an "Info" builder so you can provide additional metadata per API version.


            //                c.MultipleApiVersions(
            //                    (apiDesc, targetApiVersion) => ResolveVersionSupportByRouteConstraint(apiDesc, targetApiVersion),
            //                    (vc) =>
            //                    {
            //    vc.Version("v2", "Swashbuckle Dummy API V2");
            //    vc.Version("v1", "Swashbuckle Dummy API V1");
            //});

            //You can use "BasicAuth", "ApiKey" or "OAuth2" options to describe security schemes for the API.
            //                 See https://github.com/swagger-api/swagger-spec/blob/master/versions/2.0.md for more details.
            //                 NOTE: These only define the schemes and need to be coupled with a corresponding "security" property
            //                 at the document or operation level to indicate which schemes are required for an operation. To do this,
            //                 you'll need to implement a custom IDocumentFilter and/or IOperationFilter to set these properties
            //                 according to your specific authorization implementation

            //                c.BasicAuth("basic")
            //                    .Description("Basic HTTP Authentication");

            //NOTE: You must also configure 'EnableApiKeySupport' below in the SwaggerUI section
            //                c.ApiKey("apiKey")
            //                    .Description("API Key Authentication")
            //                    .Name("apiKey")
            //                    .In("header");

            //c.OAuth2("oauth2")
            //                    .Description("OAuth2 Implicit Grant")
            //                    .Flow("implicit")
            //                    .AuthorizationUrl("http://petstore.swagger.wordnik.com/api/oauth/dialog")
            //                    //.TokenUrl("https://tempuri.org/token")
            //                    .Scopes(scopes =>
            //                    {
            //    scopes.Add("read", "Read access to protected resources");
            //    scopes.Add("write", "Write access to protected resources");
            //});

            //Set this flag to omit descriptions for any actions decorated with the Obsolete attribute
            //                c.IgnoreObsoleteActions();

            //Each operation be assigned one or more tags which are then used by consumers for various reasons.
            //                 For example, the swagger-ui groups operations according to the first tag of each operation.
            //                 By default, this will be controller name but you can use the "GroupActionsBy" option to
            //                 override with any value.

            //                c.GroupActionsBy(apiDesc => apiDesc.HttpMethod.ToString());

            //                 You can also specify a custom sort order for groups(as defined by "GroupActionsBy") to dictate
            //                 the order in which operations are listed.For example, if the default grouping is in place
            //                 (controller name) and you specify a descending alphabetic sort order, then actions from a
            //                 ProductsController will be listed before those from a CustomersController.This is typically
            //                 used to customize the order of groupings in the swagger-ui.

            //                c.OrderActionGroupsBy(new DescendingAlphabeticComparer());

            //                 If you annotate Controllers and API Types with


        }
    }
}

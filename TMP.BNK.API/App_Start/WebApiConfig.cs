using System.Web.Http;
using System.Web.Http.Cors;
using TMP.BNK.API.Filters;

namespace TMP.BNK.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new ErrorCodeMapperAttribute());

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}

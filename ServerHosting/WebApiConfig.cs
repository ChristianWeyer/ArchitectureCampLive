using System.Net.Http.Formatting;
using System.Web.Http;

namespace ServerHosting
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var formatter = new JsonMediaTypeFormatter();
            config.Formatters.Clear();
            config.Formatters.Add(formatter);

            config.Routes.MapHttpRoute(
                "DefaultApi", "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}

using Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConferenceServices
{
    public class SpeakersService : ISpeakersService
    {
        private HttpClient client;

        public SpeakersService()
        {
            client = GetRemoteServer();
        }

        private HttpClient GetRemoteServer()
        {
            var c = new HttpClient { BaseAddress = new Uri("http://localhost:11359/") };

            return c;
        }

        private HttpClient GetLocalServer()
        {
            var s = Assembly.Load("ConferenceDudeServices");

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi", "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var server = new HttpServer(config);
            var c = new HttpClient(server) { BaseAddress = new Uri("http://can.be.anything/") };

            return c;
        }

        public async Task<IEnumerable<Speaker>> GetSpeakerListAsync()
        {
            try
            {
                var result = await client.GetAsync("api/speakers/list");
                var speakers = await result.Content.ReadAsAsync<List<Speaker>>();

                return speakers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save(Speaker speaker)
        {
        }
    }
}

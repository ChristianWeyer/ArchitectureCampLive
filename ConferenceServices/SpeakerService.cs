using System.Configuration;
using Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using ServerHosting;
using SharedContracts;

namespace ConferenceServices
{
    public class SpeakersService : ISpeakersService
    {
        private HttpClient client;

        public SpeakersService()
        {
            var serverMode = ConfigurationManager.AppSettings["confdude:serverMode"];

            if (serverMode == "Local")
            {
                client = GetLocalServer();
            }
            else
            {
                client = GetRemoteServer();   
            }
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
            WebApiConfig.Register(config);

            var server = new HttpServer(config);
            var c = new HttpClient(server) { BaseAddress = new Uri("http://can.be.anything/") };

            return c;
        }

        public async Task<IEnumerable<SpeakerDto>> GetSpeakerListAsync()
        {
            try
            {
                var result = await client.GetAsync("api/speakers/list");
                var speakers = await result.Content.ReadAsAsync<List<SpeakerDto>>();

                return speakers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save(SpeakerDto speaker)
        {
        }
    }
}

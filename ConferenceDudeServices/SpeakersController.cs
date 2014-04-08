using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace ConferenceDudeServices
{
    public class SpeakersController : ApiController
    {
        private static List<Speaker> speakers = new List<Speaker>();
        
        static SpeakersController()
        {
            var s1 = new Speaker() { Id = 1, FirstName = "Jörg", LastName = "Neumann" };
            var s2 = new Speaker() { Id = 2, FirstName = "Christian", LastName = "Weyer" };
            var s3 = new Speaker() { Id = 3, FirstName = "Ingo", LastName = "Rammer" };
            var s4 = new Speaker() { Id = 4, FirstName = "Dominick", LastName = "Baier" };
            speakers.Add(s1);
            speakers.Add(s2);
            speakers.Add(s3);
            speakers.Add(s4);
        }

        [HttpGet]
        [ActionName("ping")]
        public string Ping()
        {
            return "OK";
        }

        [HttpGet]
        [ActionName("list")]
        public List<Speaker> GetSpeakers()
        {
            return speakers;
        }

        [HttpGet]
        [ActionName("single")]
        public Speaker GetSpeakerById(int id)
        {
            var result = speakers.FirstOrDefault(s => s.Id == id);

            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return result;
        }

        [HttpPost]
        [ActionName("list")]
        public void AddSpeaker(Speaker speaker)
        {
            speakers.Add(speaker);
        }

        [HttpDelete]
        [ActionName("single")]
        public void DeleteSpeaker(int id)
        {
            speakers.RemoveAll(s => s.Id == id);
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateSpeaker(Speaker speaker)
        {
            DeleteSpeaker(speaker.Id);
            speakers.Add(speaker);
        }
    }
}
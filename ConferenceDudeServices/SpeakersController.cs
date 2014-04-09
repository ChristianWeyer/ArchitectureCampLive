using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace ConferenceDudeServices
{
    public class SpeakersController : ApiController
    {
        [HttpGet]
        [ActionName("ping")]
        public string Ping()
        {
            return "OK";
        }

        [HttpGet]
        [ActionName("list")]
        public List<SpeakerDto> GetSpeakers()
        {
            using (var db = new ConferenceDudeContext())
            {
                var speakers = db.Speakers.ToList();

                return speakers.Select(s => new SpeakerDto
                {
                    Id = s.Id,
                    FirstName = s.Name.Split(' ')[0],
                    LastName = s.Name.Split(' ')[1]
                }).ToList();
            }
        }

        //[HttpGet]
        //[ActionName("single")]
        //public Speaker GetSpeakerById(int id)
        //{
        //    var result = speakers.FirstOrDefault(s => s.Id == id);

        //    if (result == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }

        //    return result;
        //}

        //[HttpPost]
        //[ActionName("list")]
        //public void AddSpeaker(Speaker speaker)
        //{
        //    speakers.Add(speaker);
        //}

        //[HttpDelete]
        //[ActionName("single")]
        //public void DeleteSpeaker(int id)
        //{
        //    speakers.RemoveAll(s => s.Id == id);
        //}

        //[HttpPut]
        //[ActionName("list")]
        //public void UpdateSpeaker(Speaker speaker)
        //{
        //    DeleteSpeaker(speaker.Id);
        //    speakers.Add(speaker);
        //}
    }
}
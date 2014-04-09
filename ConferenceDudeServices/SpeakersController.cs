using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using SharedContracts;

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

        [HttpGet]
        [ActionName("single")]
        public SpeakerDto GetSpeakerById(int id)
        {
            using (var db = new ConferenceDudeContext())
            {
                var speaker = db.Speakers.FirstOrDefault(s => s.Id == id);

                if (speaker == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return new SpeakerDto { Id = speaker.Id, FirstName = speaker.Name.Split(' ')[0], LastName = speaker.Name.Split(' ')[1] };
            }
        }

        [HttpPost]
        [ActionName("list")]
        public void AddSpeaker(SpeakerDto speaker)
        {
            using (var db = new ConferenceDudeContext())
            {
                var entry = new Speaker { Name = speaker.FirstName + " " + speaker.LastName };
                db.Speakers.Add(entry);
                db.SaveChanges();
            }
        }

        [HttpDelete]
        [ActionName("single")]
        public void DeleteSpeaker(int id)
        {
            using (var db = new ConferenceDudeContext())
            {
                db.Entry(new Speaker { Id = id }).State =
                    EntityState.Deleted;
                db.SaveChanges();
            }
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateSpeaker(SpeakerDto speaker)
        {
            using (var db = new ConferenceDudeContext())
            {
                db.Entry(new Speaker { Id = speaker.Id, Name = speaker.FirstName + " " + speaker.LastName }).State =
                    EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
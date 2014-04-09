using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISpeakersService
    {
        Task<IEnumerable<Speaker>> GetSpeakerListAsync();
        void Save(Speaker speaker);
    }
}

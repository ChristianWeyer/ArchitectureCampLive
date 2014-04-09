using System.Collections.Generic;
using System.Threading.Tasks;
using SharedContracts;

namespace Contracts
{
    public interface ISpeakersService
    {
        Task<IEnumerable<SpeakerDto>> GetSpeakerListAsync();
        void Save(SpeakerDto speaker);
    }
}

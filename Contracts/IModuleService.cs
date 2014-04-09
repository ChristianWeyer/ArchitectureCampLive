using System.Collections.Generic;

namespace Contracts
{
    public interface IModuleService
    {
        IModule CreateModule(IModuleMetadata metadata);
        IEnumerable<Contracts.IModuleMetadata> GetModules();
    }
}

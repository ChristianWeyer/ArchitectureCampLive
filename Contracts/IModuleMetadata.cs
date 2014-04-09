using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IModuleMetadata
    {
        string Name { get; }
        string DisplayName { get; }
        int OrderIndex { get; }
    }
}

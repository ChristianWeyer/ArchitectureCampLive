using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited= true)]
    public class ModuleMetadataAttribute : ExportAttribute, IModuleMetadata
    {
        public ModuleMetadataAttribute(string name, string displayName, int orderIndex) : base(typeof(IModule))
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.OrderIndex = orderIndex;
        }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public int OrderIndex { get; private set; }

    }
}

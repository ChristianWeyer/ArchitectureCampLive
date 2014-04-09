using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceDude.Services
{
    public class ModuleService
    {
        private CompositionContainer _container;

        public ModuleService()
        {
            var path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var catalog = new DirectoryCatalog(path);
            _container = new CompositionContainer(catalog);
            _container.SatisfyImportsOnce(this);
        }

        public IEnumerable<IModuleMetadata> GetModules()
        {
            var modules = _container.GetExports<IModule, IModuleMetadata>();
            var list = new List<IModuleMetadata>();
            foreach (var lazy in modules)
            {
                list.Add(lazy.Metadata);
            }
            return list;
        }

        public IModule CreateModule(IModuleMetadata metadata)
        {
            var modules = _container.GetExports<IModule, IModuleMetadata>();
            foreach (var mod in modules)
            {
                if (mod.Metadata.Name.Equals(metadata.Name))
                {
                    var module = mod.Value;
                    module.Initialize();
                    return module;
                }
            }
            return null;
        }
    }
}

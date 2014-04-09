using ConferenceDude.Services;
using ConferenceServices;
using Contracts;
using System.Windows;

namespace ConferenceDude
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeServices();
        }

        private void InitializeServices()
        {
            ServicePool.Current.AddService<IModuleService>(new ModuleService());
            ServicePool.Current.AddService<ISpeakersService>(new SpeakersService());
        }
    }
}

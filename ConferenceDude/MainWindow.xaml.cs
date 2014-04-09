using ConferenceDude.Services;
using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConferenceDude
{
    public partial class MainWindow : Window
    {
        private ModuleService _moduleService;

        public MainWindow()
        {
            InitializeComponent();
            _moduleService = new ModuleService();
            var list = _moduleService.GetModules();
            this.modulesListBox.DisplayMemberPath = "DisplayName";
            this.modulesListBox.ItemsSource = list;
        }

        private void modulesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var metadata = e.AddedItems[0] as IModuleMetadata;
            var module = _moduleService.CreateModule(metadata);
            this.border.Child = module.GetView();
        }
    }
}

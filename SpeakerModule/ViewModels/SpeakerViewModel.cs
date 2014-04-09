using ConferenceDude.Modules.SpeakerModule.Views;
using Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ConferenceDude.Modules.SpeakerModule.ViewModels
{
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    [ModuleMetadata("SpeakerViewModel", "Speakers", 0)]
    public class SpeakerViewModel : ModelBase, IModule
    {
        private string _state;
        public string State
        {
            get { return _state; }
            set { _state = value; this.OnPropertyChanged(); }
        }
        private ISpeakersService _speakerService;
        private ICollectionView _speakerListView;
        public ICollectionView SpeakerListView
        {
            get { return _speakerListView; }
            set { _speakerListView = value; this.OnPropertyChanged(); }
        }

        private IEditableCollectionView _speakerEditView;
        private bool _isInititializing;
        public SpeakerViewModel()
        {
        }

        void SpeakerListView_CurrentChanged(object sender, System.EventArgs e)
        {
            if (_isInititializing)
            {
                return;
            }
            var speaker = this.SpeakerListView.CurrentItem as Speaker;
        }

        private bool CanExecuteSaveCommand(object parameter)
        {
            return true; //this.CurrentSpeaker != null;
        }

        private void ExecuteSaveCommand(object parameter)
        {
            this.State = "Edit";


            //if (!_speakerEditView.IsEditingItem)
            //{
            //    _speakerEditView.EditItem(this.SpeakerListView.CurrentItem);
            //}
            //else
            //{
            //    _speakerEditView.CancelEdit();
            //    //_speakerEditView.CommitEdit();
            //    //_speakerService.Save(this.CurrentSpeaker);
            //}

            //this.RefreshCommands();
        }

        private void RefreshCommands()
        {
            this.CanExecuteSaveCommand(null);
        }

        private ObservableCollection<Speaker> _speakerList;
        public ObservableCollection<Speaker> SpeakerList
        {
            get { return _speakerList; }
            set { _speakerList = value; this.OnPropertyChanged(); }
        }
        private Speaker _currentSpeaker;
        public Speaker CurrentSpeaker
        {
            get { return _currentSpeaker; }
            set { _currentSpeaker = value; this.OnPropertyChanged(); }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set { _saveCommand = value; this.OnPropertyChanged(); }
        }

        public async void Initialize(IServicePool pool)
        {
            _isInititializing = true;
            this.State = "ReadOnly";

            if (Application.Current != null &&
                Application.Current.MainWindow != null &&
                !DesignerProperties.GetIsInDesignMode(Application.Current.MainWindow))
            {
                _speakerService = pool.GetService<ISpeakersService>();
                this.SaveCommand = new DelegateCommand(this.CanExecuteSaveCommand, this.ExecuteSaveCommand);
                var list = await _speakerService.GetSpeakerListAsync();
                this.SpeakerList = new ObservableCollection<Speaker>(list);
                this.CurrentSpeaker = this.SpeakerList[0];
                this.SpeakerListView = new ListCollectionView(this.SpeakerList);
                this.SpeakerListView.CurrentChanged += SpeakerListView_CurrentChanged;
                this.SpeakerListView.MoveCurrentTo(this.SpeakerList[1]);
                //this.SpeakerListView.Filter = item => ((Speaker)item).FirstName.StartsWith("C");
                _speakerEditView = this.SpeakerListView as IEditableCollectionView;
            }

            _isInititializing = false;
        }

        public FrameworkElement GetView()
        {
            return new SpeakerView() { DataContext = this };
        }
    }
}

using ConferenceDude.Infrastructure;
using ConferenceDude.Models;
using ConferenceDude.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ConferenceDude.ViewModels
{
    public class SpeakerViewModel : ModelBase
    {
        private string _state;
        public string State
        {
            get { return _state; }
            set { _state = value; this.OnPropertyChanged(); }
        }
        private SpeakersService _speakerService;
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
            _isInititializing = true;
            this.State = "ReadOnly";

            if (Application.Current != null &&
                Application.Current.MainWindow != null &&
                !DesignerProperties.GetIsInDesignMode(Application.Current.MainWindow))
            {
                _speakerService = new SpeakersService();
                this.SaveCommand = new DelegateCommand(this.CanExecuteSaveCommand, this.ExecuteSaveCommand);
                
            }

            _isInititializing = false;
        }

        void SpeakerListView_CurrentChanged(object sender, System.EventArgs e)
        {
            if (_isInititializing)
            {
                return;
            }
            var speaker = this.SpeakerListView.CurrentItem as Speaker;
            MessageBox.Show(speaker.FirstName);
        }

        private bool CanExecuteSaveCommand(object parameter)
        {
            return true; //this.CurrentSpeaker != null;
        }

        private async void ExecuteSaveCommand(object parameter)
        {
            this.State = "Edit";

            var list = await _speakerService.GetSpeakerListAsync();
            this.SpeakerList = new ObservableCollection<Speaker>(list);
            this.CurrentSpeaker = this.SpeakerList[0];
            this.SpeakerListView = new ListCollectionView(this.SpeakerList);
            this.SpeakerListView.CurrentChanged += SpeakerListView_CurrentChanged;
            this.SpeakerListView.MoveCurrentTo(this.SpeakerList[1]);
            //this.SpeakerListView.Filter = item => ((Speaker)item).FirstName.StartsWith("C");
            _speakerEditView = this.SpeakerListView as IEditableCollectionView;

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
    }
}

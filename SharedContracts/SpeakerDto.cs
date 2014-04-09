using System.ComponentModel;

namespace SharedContracts
{
    public class SpeakerDto : ModelBase, IEditableObject
    {
        public int Id { get; set; }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; this.OnPropertyChanged(); }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; this.OnPropertyChanged(); }
        }

        private string _firstNameCache;
        private string _lastNameCache;
        public void BeginEdit()
        {
            _firstNameCache = _firstName;
        }

        public void CancelEdit()
        {
            this.FirstName = _firstNameCache;
        }

        public void EndEdit()
        {
            _firstNameCache = null;
        }
    }
}

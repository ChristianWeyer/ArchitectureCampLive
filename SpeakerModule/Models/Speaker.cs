using ConferenceDude.Modules.SpeakerModule.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceDude.Modules.SpeakerModule.Models
{
    public class Speaker : ModelBase, IEditableObject
    {
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

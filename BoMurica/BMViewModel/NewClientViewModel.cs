using BoMurica.BMCommands;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BoMurica.BMViewModel
{
    internal class NewClientViewModel : ViewModelBase
    {
        private CurrentUserService _currentUser;
        public ICommand CancelCommand { get; }
        public ICommand CreateCommand { get; }
        private ClientInfo _newClient = new ClientInfo();

        public CurrentUserService CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public ClientInfo NewClient
        {
            get { return _newClient; }
            set { _newClient = value; }
        }

        public string FirstName
        {
            get { return _newClient.FirstName; }
            set
            {
                _newClient.FirstName = value;
                OnPropertyChanged(nameof(FirstName));

            }
        }

        public string LastName
        {
            get { return _newClient.LastName; }
            set
            {
                _newClient.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Phone
        {
            get { return _newClient.Phone; }
            set
            {
                _newClient.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public string Email
        {
            get { return _newClient.Email; }
            set
            {
                _newClient.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public ICommand SignOutPageCommand { get; }
        public NewClientViewModel(NavigationStore navigationStore, Func<AdminHomeViewModel> createAdminHomeViewModel)
        {
            _currentUser = CurrentUserService.Instance(null);
            CreateCommand = new CreateClientCommand(this,navigationStore, createAdminHomeViewModel);
            CancelCommand = new NavigateCommand(navigationStore, createAdminHomeViewModel);
            SignOutPageCommand = new NavigateCommand(navigationStore, createAdminHomeViewModel);
    }

    }
}


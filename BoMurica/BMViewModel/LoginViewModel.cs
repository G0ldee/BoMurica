using BoMurica.BMCommands;
using BoMurica.BMModels;
using BoMurica.BMView;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BoMurica.BMViewModel
{
    internal class LoginViewModel : ViewModelBase
    {
        User user = new User();
        private CurrentUserService _currentUser;
        private Creds _creds = new Creds();
        public Creds Creds { get { return _creds; } }
        public string Code
        {
            get { return _creds.code; }
            set
            {
                _creds.code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
        public string Pass
        {
            get { return _creds.pass; }
            set
            {
                _creds.pass = value;
                OnPropertyChanged(nameof(Pass));
            }
        }      
        public ICommand LoginCommand { get; }

        public LoginViewModel(NavigationStore navigationStore, Func<AdminHomeViewModel> createAdminHomViewModel, Func<ClientHomeViewModel> createClientHomeViewModel)
        {
            _currentUser = CurrentUserService.Instance(user);
            LoginCommand = new LoginCommand(this, navigationStore, createAdminHomViewModel, createClientHomeViewModel);
        }

    }
    internal class Creds
    {

        public string code;
        public string pass;

        public Creds()
        {
            code= string.Empty;
            pass= string.Empty;
        }
    }
}

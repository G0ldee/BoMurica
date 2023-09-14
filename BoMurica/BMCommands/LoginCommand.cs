using BoMurica.BMModels;
using BoMurica.BMView;
using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoMurica.BMCommands
{
    internal class LoginCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private CurrentUserService _currentUser  = CurrentUserService.Instance(null);
        private readonly Func<ViewModelBase> _createAdminViewModel;
        private readonly Func<ViewModelBase> _createClientViewModel;

        private AdminLoginCred currentAdminLoginCred = new AdminLoginCred();
        private ClientLoginCred currentClientLoginCred = new ClientLoginCred();
        private NavigateCommand login;
        Creds currentCreds = new Creds();

        public LoginCommand(LoginViewModel loginViewModel, Stores.NavigationStore navigationStore, 
            Func<AdminHomeViewModel> createAdminHomeViewModel, Func<ClientHomeViewModel> createClientHomeViewModel)
        {
            _navigationStore = navigationStore;
            _createAdminViewModel = createAdminHomeViewModel;
            _createClientViewModel = createClientHomeViewModel;
            loginViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.Code) ||
                e.PropertyName == nameof(LoginViewModel.Pass))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object logincredentials)
        {
            if (logincredentials != null)
            {
                currentCreds = logincredentials as Creds;
                if (!String.IsNullOrEmpty(currentCreds.code) && !String.IsNullOrEmpty(currentCreds.pass))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Execute(object parameter)
        {
            var creds = parameter as Creds;
            User user = GetUser(creds.code);
            if (!String.IsNullOrEmpty(user.FirstName))
            {
                _currentUser.User = user;

                if (user.IsAdmin)
                {
                    if (currentAdminLoginCred.AdPassword.ToString() == creds.pass)
                    {
                        login = new NavigateCommand(_navigationStore, _createAdminViewModel);
                        login.Execute(null);
                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials!!!");
                    }
                }
                else if (!user.IsAdmin)
                {
                    SuspendedAccount suspended = new SuspendedAccount();
                    ATM atm = new ATM();
                    using (BomDBEntities1 bomDb = new BomDBEntities1())
                    {
                        suspended = bomDb.SuspendedAccounts.FirstOrDefault(u => u.ClientID == currentClientLoginCred.ClientID);
                        atm = bomDb.ATMs.First();
                    }

                    if (currentClientLoginCred.ClPassword.ToString() == creds.pass)
                    {
                        if (suspended == null)
                        {
                            if (atm.Active)
                            {
                                login = new NavigateCommand(_navigationStore, _createClientViewModel);
                                login.Execute(null);
                            }
                        }else
                            MessageBox.Show("Suspended!!!");
                    }
                    else
                    {
                        if (_currentUser.User.Attemps <= 3)
                        {
                            _currentUser.User.Attemps = _currentUser.User.Attemps + 1;
                            MessageBox.Show("Invalid Credentials!!!");
                            MessageBox.Show(_currentUser.User.Attemps.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Suspended!!!");
                        }
                    }
                }
            }
        }

        private User GetUser(object credentials)
        {
            User user = new User();

            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                currentAdminLoginCred = bomDb.AdminLoginCreds.FirstOrDefault(u => u.UserCode == (String)credentials);
                currentClientLoginCred = bomDb.ClientLoginCreds.FirstOrDefault(u => u.UserCode == (String)credentials);

                if (currentAdminLoginCred != null)
                {
                    AdminInfo userInfo = new AdminInfo();
                    userInfo = bomDb.AdminInfoes.FirstOrDefault(u => u.AdminID == currentAdminLoginCred.AdminID);
                    user.FirstName = userInfo.FirstName;
                    user.LastName = userInfo.LastName;
                    user.Id = userInfo.AdminID;
                    user.IsAdmin = true;
                }
                if (currentClientLoginCred != null)
                {
                    ClientInfo userInfo = new ClientInfo();
                    userInfo = bomDb.ClientInfoes.FirstOrDefault(u => u.ClientID == currentClientLoginCred.ClientID);
                    user.FirstName = userInfo.FirstName;
                    user.LastName = userInfo.LastName;
                    user.Id = userInfo.ClientID;
                    user.IsAdmin = false;
                }
            }
            return user;
        }


    }
}

using BoMurica.BMModels;
using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoMurica.BMCommands
{
    internal class SuspendAccountCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        private Func<AdminHomeViewModel> _createViewModel;
        private CurrentUserService _currentUser;
        public NavigateCommand nav;

        public SuspendAccountCommand(AccountStateViewModel newAccountStateViewModel, NavigationStore navigationStore, Func<AdminHomeViewModel> createHomeViewModel)
        {
            _currentUser = CurrentUserService.Instance(null);
            _navigationStore = navigationStore;
            _createViewModel = createHomeViewModel;
            newAccountStateViewModel.PropertyChanged += OnViewModelPropertyChanged;

        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AccountStateViewModel.Source) 
                || e.PropertyName == nameof(AccountStateViewModel.Active))
            {
                OnCanExecuteChanged();
            }
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            SuspendedAccount toSuspend = new SuspendedAccount();
            //MessageBox.Show(parameter.ToString());
            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                var account = bomDb.SuspendedAccounts.FirstOrDefault
                                (u => u.ClientID == parameter.ToString());
                if (account != null)
                {
                    bomDb.SuspendedAccounts.Remove(account);
                    bomDb.SaveChanges();
                    //account.ActivateAccount();
                }
                else
                {
                    toSuspend.SetValues(parameter.ToString(), "ALL", _currentUser.User.Id);
                    toSuspend.SuspendAccount();
                }

            }
            nav = new NavigateCommand(_navigationStore, _createViewModel);
            nav.Execute(null);
        }
    }
}

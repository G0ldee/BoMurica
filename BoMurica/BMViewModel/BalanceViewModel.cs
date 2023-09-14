using BoMurica.BMCommands;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BoMurica.BMViewModel
{
    internal class BalanceViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        Func<ViewModelBase> _createHomeViewModel;
        private CurrentUserService _currentUser;

        private ObservableCollection<ViewModelBase> _accounts = new ObservableCollection<ViewModelBase>();
        public IEnumerable<ViewModelBase> Accounts => _accounts; 
        public ICommand Cancel { get; }
        public BalanceViewModel(NavigationStore _navigationStore, Func<ClientHomeViewModel> createClientHomeViewModel)
        {
            _currentUser = CurrentUserService.Instance(null);

            //List<CheckingAccount>
            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                var chk = bomDb.CheckingAccounts.Where(u => u.ClientID == _currentUser.User.Id).ToList();
                foreach (var accountc in chk)
                {
                    CheckingAccoungViewModel CheckingVm = new CheckingAccoungViewModel(accountc);
                    _accounts.Add(CheckingVm);
                }
                var sv = bomDb.SavingAccounts.Where(u => u.ClientID == _currentUser.User.Id).ToList();
                foreach (var accountS in sv)
                {
                    SavingsAccountViewModel SavingVm = new SavingsAccountViewModel(accountS);
                    _accounts.Add(SavingVm);
                }
                var cr = bomDb.CreditLineAccounts.Where(u => u.ClientID == _currentUser.User.Id).ToList();
                foreach (var accountC in cr)
                {
                    CreditLineViewModel CreditVm = new CreditLineViewModel(accountC);
                    _accounts.Add(CreditVm);
                }
                var mg = bomDb.MortgageAccounts.Where(u => u.ClientID == _currentUser.User.Id).ToList();
                foreach (var account in mg)
                {
                    MortgageViewModel MortgageVm = new MortgageViewModel(account);
                    _accounts.Add(MortgageVm);
                }
                
            }
            this._navigationStore = _navigationStore;
            _createHomeViewModel = createClientHomeViewModel;
            Cancel = new NavigateCommand(_navigationStore, createClientHomeViewModel);


        }
    }
}

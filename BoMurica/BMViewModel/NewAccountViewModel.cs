using BoMurica.BMCommands;
using BoMurica.BMModels;
using BoMurica.BMServices;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BoMurica.BMViewModel
{
    internal class NewAccountViewModel : ViewModelBase
    {
        private CurrentUserService _currentUser;
        private String _RBtnChk = "Checking";
        private String _RBtnSav = "Saving";
        private String _RBtnCred = "Credit";
        private String _RBtnMor = "Mortgage";
        private NewAccountService _newAccount;
        private Account account = new Account();
             
        public CurrentUserService CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        public String RBtnChk
        {
            get { return _RBtnChk; }
        }
        public String RBtnSav
        {
            get { return _RBtnSav; }
        }
        public String RBtnCred
        {
            get { return _RBtnCred; }
        }
        public String RBtnMor
        {
            get { return _RBtnMor; }
        }

        public Account NewAccount { get { return _newAccount.Account; } }

        public String ClientId
        {
            get { return _newAccount.Account.ClientId; }
            set
            {
                _newAccount.Account.ClientId = value;
                OnPropertyChanged(nameof(ClientId));
            }
        }
        public String Type
        {
            get { return _newAccount.Account.ClientId; }
            set
            {
                _newAccount.Account.Type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        public Decimal InitDeposit
        {
            get { return _newAccount.Account.InitDepositDebt; }
            set 
            { 
                _newAccount.Account.InitDepositDebt = value;
                OnPropertyChanged(nameof(InitDeposit));                
            }
        }
        public ICommand SetSelectionCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand SignOutPageCommand { get; }

        public NewAccountViewModel(NavigationStore navigationStore, Func<AdminHomeViewModel> createAdminHomeViewModel)
        {
            _currentUser = CurrentUserService.Instance(null);
            _newAccount = NewAccountService.Instance(account);
            SetSelectionCommand = new AccountSelectionCommand(this);
            CreateCommand = new CreateAccountCommand(this, navigationStore, createAdminHomeViewModel);
            CancelCommand= new NavigateCommand(navigationStore, createAdminHomeViewModel);
            //CreateCommand = new CreateAccountCommand();
            SignOutPageCommand = new NavigateCommand(navigationStore, createAdminHomeViewModel);
    }
    }
}

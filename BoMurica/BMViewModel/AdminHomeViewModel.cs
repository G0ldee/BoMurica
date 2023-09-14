using BankOfMerica.BMView;
using BoMurica.BMCommands;
using BoMurica.BMModels;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BoMurica.BMViewModel
{
    internal class AdminHomeViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private CurrentUserService _currentUser;
        private String _varSavings = "Savings";
        private String _varCredit = "Credit";
        private ATM _atm = new ATM();
        private AtmTmpData _atmTmp = new AtmTmpData();
        public NavigationStore navigationStore
        {
            get { return _navigationStore; }
            set { _navigationStore = value; }
        }
        public CurrentUserService CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        public ATM Atm { get { return _atm; } }
        public AtmTmpData AtmTmp { get { return _atmTmp; } }
        public Decimal Amount
        {
            get { return _atmTmp.amount; }
            set
            {
                _atmTmp.amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
        public bool Active
        {
            get { return _atmTmp.active; }
            set
            {
                _atmTmp.active = value;
                OnPropertyChanged(nameof(Active));
            }
        }
        public String VarSavings { get { return _varSavings; } }
        public String VarCredit { get { return _varCredit; } }

        public ICommand CreateNewClientCommand { get; }
        public ICommand Enter { get; }
        public ICommand CancelCommand { get; }
        public ICommand SetInterest { get; }
        public ICommand OpenClientForm { get; }
        public ICommand OpenAccountForm { get; }
        public ICommand SignOutPageCommand { get; }
        public ICommand OpenAtmState { get; }
        public ICommand OpenAccountState { get; }
        public ICommand OpenWithdrawMortgage { get; }
        public ICommand OpenListTransactions { get; }

        public AdminHomeViewModel
            (NavigationStore navigationStore, 
            Func<LoginViewModel> createLoginViewModel, Func<NewClientViewModel> createNewClientViewModel, 
            Func<NewAccountViewModel> createNewAccountViewModel, Func<AccountStateViewModel> createAccountStateViewModel,
            Func<AtmStateViewModel> createAtmStateViewModel, Func<WithdrawMortgageViewModel> createWithdrawalMorgageViewModel,
            Func<TransactionListViewModel> createTransactionListViewModel)
        {
            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                _atm = bomDb.ATMs.First();
                _atmTmp.active = _atm.Active;
            }
            this.navigationStore = navigationStore;
            _currentUser = CurrentUserService.Instance(null);
            OpenClientForm = new NavigateCommand(navigationStore, createNewClientViewModel);
            OpenAccountForm = new NavigateCommand(navigationStore, createNewAccountViewModel);
            OpenAtmState = new NavigateCommand(navigationStore, createAtmStateViewModel);
            OpenAccountState = new NavigateCommand(navigationStore, createAccountStateViewModel);
            OpenWithdrawMortgage = new NavigateCommand(navigationStore, createWithdrawalMorgageViewModel);
            OpenListTransactions = new NavigateCommand(navigationStore, createTransactionListViewModel);
            SetInterest = new SetInterestCommand();
            Enter = new UpdateAtmCommand(this);
            SignOutPageCommand = new NavigateCommand(_navigationStore, createLoginViewModel);
        }
    }
    internal class AtmTmpData
    {
        public Decimal amount;
        public bool active;

        public AtmTmpData()
        {
            amount = 0.00m;
            active = true;
        }
    }
}

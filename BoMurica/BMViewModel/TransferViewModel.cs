using BoMurica.BMCommands;
using BoMurica.BMModels;
using BoMurica.BMServices;
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
    internal class TransferViewModel : ViewModelBase
    {

        private NavigationStore _navigationStore;
        private CurrentUserService _currentUser;
        private TransactionService _currentTransaction;
        private List<String> _accountList;
        private List<String> _userCheckingList;
        Func<ClientHomeViewModel> _createHomeViewModel;
        private String _varSavings = "Savings";
        private String _varCredit = "Credit";
        public String VarSavings
        {
            get { return _varSavings; }
        }
        public String VarCredit
        {
            get { return _varCredit; }
        }
        public CurrentUserService CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        public List<String> AccountList
        {
            get { return _accountList; }
            set { _accountList = value; }
        }
        public List<String> CheckingList
        {
            get { return _userCheckingList; }
            set { _userCheckingList = value; }
        }
        public TransactionData CurrentTransaction
        {
            get { return _currentTransaction.Transaction; }
            set
            {
                _currentTransaction.Transaction = value;
                OnPropertyChanged(nameof(CurrentTransaction));
            }
        }

        public Decimal Amount
        {
            get { return _currentTransaction.Transaction.Amount; }
            set
            {
                _currentTransaction.Transaction.Amount = value;
                OnPropertyChanged(nameof(Amount));

            }
        }
        public string Source
        {
            get { return _currentTransaction.Transaction.Source; }
            set
            {
                _currentTransaction.Transaction.Source = value;
                OnPropertyChanged(nameof(Source));
            }
        }
        public string Recipient
        {
            get { return _currentTransaction.Transaction.Destination; }
            set
            {
                _currentTransaction.Transaction.Destination = value;
                OnPropertyChanged(nameof(Recipient));
            }
        }
        public ICommand Enter { get; }
        public ICommand Cancel { get; }


        public TransferViewModel(NavigationStore _navigationStore, Func<ClientHomeViewModel> createClientHomeViewModel)
        {
            _currentUser = CurrentUserService.Instance(null);
            TransactionData transaction = new TransactionData();
            _accountList = new List<String>();
            _userCheckingList = new List<String>();
            List<CheckingAccount> _checkingList = new List<CheckingAccount>();
            List<SavingAccount> _savingList = new List<SavingAccount>();
            List<MortgageAccount> _mortgageList = new List<MortgageAccount>();
            List<CreditLineAccount> _creditList = new List<CreditLineAccount>();
            _currentTransaction = TransactionService.Instance(transaction);
            String userId = _currentUser.User.Id;
            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                _checkingList = bomDb.CheckingAccounts.Where(u => u.ClientID == userId).ToList();
                _savingList = bomDb.SavingAccounts.Where(u => u.ClientID == userId).ToList();
                _mortgageList = bomDb.MortgageAccounts.Where(u => u.ClientID == userId).ToList();
                _creditList = bomDb.CreditLineAccounts.Where(u => u.ClientID == userId).ToList();
            }
            _checkingList.ForEach(y => { _accountList.Add(y.Account); _userCheckingList.Add(y.Account); });
            _savingList.ForEach(y => { _accountList.Add(y.Account); });
            _mortgageList.ForEach(y => { _accountList.Add(y.Account); });
            _creditList.ForEach(y => { _accountList.Add(y.Account); });
            this._navigationStore = _navigationStore;
            _createHomeViewModel = createClientHomeViewModel;
            Enter = new TransferCommand(_currentUser.User.Id, this, _navigationStore, _createHomeViewModel);
            Cancel = new CancelCommand(_navigationStore, _createHomeViewModel);

        }
    }
}

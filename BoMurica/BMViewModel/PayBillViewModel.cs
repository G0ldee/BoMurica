using BoMurica.BMCommands;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using BoMurica.BMServices;
using BoMurica.BMModels;

namespace BoMurica.BMViewModel
{
    internal class PayBillViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private CurrentUserService _currentUser;
        private TransactionService _currentTransaction;
        private List<String> _accountList;
        Func<ClientHomeViewModel> _createHomeViewModel;

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


        public PayBillViewModel(NavigationStore _navigationStore, Func<ClientHomeViewModel> createClientHomeViewModel)
        {
            _currentUser = CurrentUserService.Instance(null);
            TransactionData transaction = new TransactionData();
            _accountList = new List<String>();
            List<CheckingAccount> _checkingList = new List<CheckingAccount>();
            List<SavingAccount> _savingList = new List<SavingAccount>();
            List<MortgageAccount> _mortgageList = new List<MortgageAccount>();
            _currentTransaction = TransactionService.Instance(transaction);
            String userId = _currentUser.User.Id;
            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                _checkingList = bomDb.CheckingAccounts.Where(u => u.ClientID == userId).ToList();
            }
            _checkingList.ForEach(y => { _accountList.Add(y.Account); });
            this._navigationStore = _navigationStore;
            _createHomeViewModel = createClientHomeViewModel;
            Enter = new TransferCommand(_currentUser.User.Id, this, _navigationStore, _createHomeViewModel);
            Cancel = new CancelCommand(_navigationStore, _createHomeViewModel);
        }
    }
}

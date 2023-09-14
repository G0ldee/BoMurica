﻿using BoMurica.BMCommands;
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
    internal class WithdrawViewModel : ViewModelBase
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
        public decimal Amount
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
        public ICommand Enter { get; }
        public ICommand Cancel { get; }


        public WithdrawViewModel(NavigationStore _navigationStore, Func<ClientHomeViewModel> createClientHomeViewModel)
        {
            _currentUser = CurrentUserService.Instance(null);
            TransactionData transaction = new TransactionData();
            _accountList= new List<String>();
            List<CheckingAccount> _checkingList = new List<CheckingAccount>();
            List<SavingAccount> _savingList = new List<SavingAccount>();
            _currentTransaction = TransactionService.Instance(transaction);
            String userId = _currentUser.User.Id;
            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                _checkingList = bomDb.CheckingAccounts.Where(u => u.ClientID == userId).ToList();
                _savingList = bomDb.SavingAccounts.Where(u => u.ClientID == userId).ToList();               
            }
            _checkingList.ForEach(y => { _accountList.Add(y.Account); }) ;
            _savingList.ForEach(y => { _accountList.Add(y.Account); }) ;
            this._navigationStore = _navigationStore;
            _createHomeViewModel = createClientHomeViewModel;
            Enter = new WithdrawCommand(_currentUser.User.Id, this, _navigationStore, _createHomeViewModel);
            Cancel = new CancelCommand(_navigationStore, _createHomeViewModel);
        }    
    }
}

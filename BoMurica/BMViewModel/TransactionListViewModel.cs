using BoMurica.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using BoMurica.BMCommands;
using System.Windows.Input;
using System.Linq;

namespace BoMurica.BMViewModel
{
    internal class TransactionListViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        Func<AdminHomeViewModel> _createHomeViewModel;
        private CurrentUserService _currentUser;
        private List<String> _accountList = new List<String>();

        private ObservableCollection<TransactionViewModel> _transactions = new ObservableCollection<TransactionViewModel>();

        public IEnumerable<TransactionViewModel> Transactions => _transactions;
        public List<String> AccountList
        {
            get { return _accountList; }
            set { _accountList = value; }
        }
        public string Source
        {
            get { return _currentUser.User.Selection; }
            set
            {
                _currentUser.User.Selection = value;
                OnPropertyChanged(nameof(Source));
                UpdateListView();
            }

        }

        private void UpdateListView()
        {
            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                var tmpTransactions = bomDb.Transactions.Where(u => u.ClientID == Source).ToList();
                foreach (var transaction in tmpTransactions)
                {
                    TransactionViewModel tmp = new TransactionViewModel(transaction);
                    _transactions.Add(tmp);
                }
            }
        }

        public ICommand Cancel { get; }
        public ICommand SignOutPageCommand { get; }


        public TransactionListViewModel(NavigationStore _navigationStore, Func<AdminHomeViewModel> createAdminHomeViewModel)
        {
            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                var _tmpAccountList = bomDb.ClientInfoes.ToList();
                _tmpAccountList.ForEach(y => { _accountList.Add(y.ClientID.ToString()); });
            }
            this._navigationStore = _navigationStore;
            _currentUser = CurrentUserService.Instance(null);
            _createHomeViewModel = createAdminHomeViewModel;
            Cancel = new NavigateCommand(_navigationStore, createAdminHomeViewModel);
            SignOutPageCommand = new NavigateCommand(_navigationStore, createAdminHomeViewModel);
    }

    }
}
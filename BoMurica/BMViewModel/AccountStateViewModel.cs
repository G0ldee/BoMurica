using BoMurica.BMCommands;
using BoMurica.BMModels;
using BoMurica.BMServices;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;

namespace BoMurica.BMViewModel
{
    internal class AccountStateViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private CurrentUserService _currentUser;
        private SuspendedAccount _account = new SuspendedAccount();
        private bool _active = true;
        private List<String> _clientList;

        public List<String> AccountList
        {
            get { return _clientList; }
            set { _clientList = value; }
        }
        public string Source
        {
            get { return _account.AccountID; }
            set
            {
                _account.AccountID = value;
                OnPropertyChanged(nameof(Source));
            }
        }
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged(nameof(Active));
            }
        }
        public ICommand Enter { get; }
        public ICommand Cancel { get; }
        public ICommand SignOutPageCommand { get; }

        public AccountStateViewModel(NavigationStore _navigationStore, Func<AdminHomeViewModel> createHomeViewModel)
        {

            TransactionData transaction = new TransactionData();
            _clientList = new List<String>();
            List<ClientInfo> _clientInfoList = new List<ClientInfo>();

            this._navigationStore = _navigationStore;
            _currentUser = CurrentUserService.Instance(null);
            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                _clientInfoList = bomDb.ClientInfoes.ToList();
                
            }
            _clientInfoList.ForEach(client => { _clientList.Add(client.ClientID); });
            Enter = new SuspendAccountCommand(this, _navigationStore, createHomeViewModel);
            Cancel = new CancelCommand(_navigationStore, createHomeViewModel);
            SignOutPageCommand = new NavigateCommand(_navigationStore, createHomeViewModel);
    }
    }
}

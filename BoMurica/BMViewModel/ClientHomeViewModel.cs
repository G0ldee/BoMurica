using BoMurica.BMCommands;
using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BoMurica.BMViewModel
{
    internal class ClientHomeViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private CurrentUserService _currentUser;
        Func<LoginViewModel> _createLoginViewModel;

        public CurrentUserService CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public ICommand WithdrawPageCommand { get; }
        public ICommand DepositPageCommand { get; }
        public ICommand TransactionsPageCommand { get; }
        public ICommand TransferPageCommand { get; }
        public ICommand PayBillPageCommand { get; }
        public ICommand SignOutPageCommand { get; }

        public ClientHomeViewModel(NavigationStore _navigationStore, 
            Func<LoginViewModel> createLoginViewModel, Func<WithdrawViewModel> createWithdrawViewModel,
             Func<DepositViewModel> createDepositViewModel, Func<TransferViewModel> createTransferViewModel,
              Func<PayBillViewModel> createPayBillViewModel, Func<BalanceViewModel> createBalanceViewModel) 
        {
            this._navigationStore = _navigationStore;
            _currentUser = CurrentUserService.Instance(null);
            _createLoginViewModel = createLoginViewModel;
            WithdrawPageCommand = new OpenWithdrawViewCommand(_navigationStore, createWithdrawViewModel);
            DepositPageCommand = new OpenDepositViewCommand(_navigationStore, createDepositViewModel);
            TransferPageCommand = new OpenTransferPageCommand(_navigationStore, createTransferViewModel);
            PayBillPageCommand = new OpenPayBillPageCommand(_navigationStore, createPayBillViewModel);
            TransactionsPageCommand = new NavigateCommand(_navigationStore, createBalanceViewModel);
            SignOutPageCommand = new NavigateCommand(_navigationStore, createLoginViewModel);
        }
    }
}
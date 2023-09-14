using BoMurica.BMModels;
using BoMurica.BMView;
using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BoMurica
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        //private readonly CurrentUserService _CurrentUserStore;
        //private BMModels.User user;

        public App()
        {
            _navigationStore= new NavigationStore();
            //_CurrentUserStore= new CurrentUserService();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //_navigationStore.CurrentViewModel = new AdminHomeViewModel();
            _navigationStore.CurrentViewModel = CreateLoginViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();


            base.OnStartup(e);
        }

        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(_navigationStore, CreateAdminHomeViewModel, CreateClientHomeViewModel);
        }
       
        
        private AdminHomeViewModel CreateAdminHomeViewModel()
        {
            return new AdminHomeViewModel(_navigationStore, CreateLoginViewModel, CreateNewClientViewModel, CreateNewAccountViewModel, CreateAccountStateViewModel, CreateAtmStateViewModel, CreateWithdrawMortgageViewModel, CreateTransactionsViewModell);
        }
        private NewClientViewModel CreateNewClientViewModel()
        {
            return new NewClientViewModel(_navigationStore, CreateAdminHomeViewModel);
        }
        private NewAccountViewModel CreateNewAccountViewModel()
        {
            return new NewAccountViewModel(_navigationStore, CreateAdminHomeViewModel);
        }
        private AccountStateViewModel CreateAccountStateViewModel()
        {
            return new AccountStateViewModel(_navigationStore, CreateAdminHomeViewModel);
        }
        private AtmStateViewModel CreateAtmStateViewModel()
        {
            return new AtmStateViewModel(_navigationStore, CreateAdminHomeViewModel);
        }
        private WithdrawMortgageViewModel CreateWithdrawMortgageViewModel()
        {
            return new WithdrawMortgageViewModel(_navigationStore, CreateAdminHomeViewModel);
        }
        private TransactionListViewModel CreateTransactionsViewModell()
        {
            return new TransactionListViewModel(_navigationStore, CreateAdminHomeViewModel);
        }



        private ClientHomeViewModel CreateClientHomeViewModel()
        {
            return new ClientHomeViewModel(_navigationStore, CreateLoginViewModel, CreateWithdrawViewModel, CreateDepositViewModel, CreateTransferViewModel, CreatePayBillViewModel, CreateBalanceViewModel);
        }        
        private WithdrawViewModel CreateWithdrawViewModel()
        {
            return new WithdrawViewModel(_navigationStore, CreateClientHomeViewModel);
        }
        private DepositViewModel CreateDepositViewModel()
        {
            return new DepositViewModel(_navigationStore, CreateClientHomeViewModel);
        }
        private TransferViewModel CreateTransferViewModel()
        {
            return new TransferViewModel(_navigationStore, CreateClientHomeViewModel);
        }
        private PayBillViewModel CreatePayBillViewModel()
        {
            return new PayBillViewModel(_navigationStore, CreateClientHomeViewModel);
        }
        private BalanceViewModel CreateBalanceViewModel()
        {
            return new BalanceViewModel(_navigationStore, CreateClientHomeViewModel);
        }
    }
}

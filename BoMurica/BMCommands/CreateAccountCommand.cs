using BoMurica.BMModels;
using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoMurica.BMCommands
{
    internal class CreateAccountCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        private Func<AdminHomeViewModel> _createViewModel;
        public NavigateCommand nav;
        Account currentNewAccount = new Account();

        public CreateAccountCommand(NewAccountViewModel newAccountViewModel,NavigationStore navigationStore, Func<BMViewModel.AdminHomeViewModel> createHomeViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createHomeViewModel;
            newAccountViewModel.PropertyChanged += OnViewModelPropertyChanged;
            
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(NewAccountViewModel.NewAccount.ClientId) || e.PropertyName == nameof(NewAccountViewModel.NewAccount.Type))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object account)
        {
            if(account != null)
            {
                currentNewAccount = account as Account;
                if(!String.IsNullOrEmpty(currentNewAccount.ClientId) && !String.IsNullOrEmpty(currentNewAccount.Type))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Execute(object account)
        {
            currentNewAccount = account as Account;
            ClientInfo client;
            using(BomDBEntities1 bomDb = new BomDBEntities1())
            {
                client = bomDb.ClientInfoes.FirstOrDefault(u=> u.ClientID== currentNewAccount.ClientId);
            }
            if (client != null)
            {
                switch (currentNewAccount.Type)
                {
                    case "Checking":
                        var newChAccount = new CheckingAccount();
                        newChAccount.SetValues(currentNewAccount);
                        newChAccount.AddCheckingAccount();
                        break;

                    case "Saving":
                        var newSvAccount = new SavingAccount();
                        newSvAccount.SetValues(currentNewAccount);
                        newSvAccount.AddSavingAccount();
                        break;

                    case "Credit":
                        var newCrAccount = new CreditLineAccount();
                        newCrAccount.SetValues(currentNewAccount);
                        newCrAccount.AddCreditAccount();
                        break;

                    case "Mortgage":
                        var newMgAccount = new MortgageAccount();
                        newMgAccount.SetValues(currentNewAccount);
                        newMgAccount.AddMortgageAccount();
                        break;
                    default:
                        break;
                }
                nav = new NavigateCommand(_navigationStore, _createViewModel);
                nav.Execute(null);
            }
            else
                MessageBox.Show("Client does not exist! Please verify ClientId and try again");
        }
    }
}

using BoMurica.BMModels;
using BoMurica.BMServices;
using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BoMurica.BMCommands
{
    internal class DepositCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        private Func<ClientHomeViewModel> _createHomeViewModel;
        private TransactionService _currentTransaction;
        private String _currentUser;
        private ICommand nav;

        public DepositCommand(String user, DepositViewModel depositViewModel, NavigationStore navigationStore, Func<ClientHomeViewModel> createHomeViewModel)
        {
            _currentUser = user;
            _navigationStore = navigationStore;
            _createHomeViewModel = createHomeViewModel;
            _currentTransaction = TransactionService.Instance(null);
            depositViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DepositViewModel.Amount) || 
                e.PropertyName == nameof(DepositViewModel.Source))
            {
                OnCanExecuteChanged();
            }
        }
        public override bool CanExecute(object parameter)
        {
            if (!String.IsNullOrEmpty(_currentTransaction.Transaction.Source) && _currentTransaction.Transaction.Amount > 0)
            {
                return true;
            }
            else
                return false;
        }

        public override void Execute(object parameter)
        {
            var typeSourceID = _currentTransaction.Transaction.Source.Substring(0, 2);
            using (BomDBEntities1 bomDb = new BomDBEntities1())
                if (_currentTransaction.Transaction.Amount >= 0.00m)
                {
                    switch (typeSourceID)
                    {
                        case "CH":
                            var accountCSource = bomDb.CheckingAccounts.FirstOrDefault
                                (u => u.Account == _currentTransaction.Transaction.Source);

                            if (accountCSource != null)
                            {
                                if (accountCSource.UpdateBalance(_currentTransaction.Transaction.Amount))
                                {
                                    bomDb.SaveChanges();
                                    RecordTransaction();
                                }
                            }
                            break;

                        case "SV":
                            var accountSSource = bomDb.SavingAccounts.FirstOrDefault
                                (u => u.Account == _currentTransaction.Transaction.Source);

                            if (accountSSource != null)
                            {
                                if (accountSSource.UpdateBalance(_currentTransaction.Transaction.Amount))
                                {
                                    bomDb.SaveChanges();
                                    RecordTransaction();
                                }
                            }
                            break;
                        case "MG":
                            var accountSource = bomDb.MortgageAccounts.FirstOrDefault
                                (u => u.Account == _currentTransaction.Transaction.Source);

                            if (accountSource != null)
                            {
                                if (accountSource.UpdateBalance(_currentTransaction.Transaction.Amount))
                                {
                                    bomDb.SaveChanges();
                                    RecordTransaction();
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            nav = new NavigateCommand(_navigationStore, _createHomeViewModel);
            nav.Execute(null);
            _currentTransaction.Transaction = new TransactionData();
        }
        private void RecordTransaction()
        {
            _currentTransaction.Transaction.Out = true;
            _currentTransaction.Transaction.Destination = "CASH/IN";
            _currentTransaction.Transaction.ClientId = _currentUser;
            var transaction = new Transaction();
            transaction.SetValues(_currentTransaction.Transaction);
            transaction.AddTransaction();
        }
    }
}

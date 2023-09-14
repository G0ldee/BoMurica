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
using System.Windows.Documents;
using System.Windows.Input;

namespace BoMurica.BMCommands
{
    internal class WithdrawCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        private Func<ViewModelBase> _createHomeViewModel;
        private TransactionService _currentTransaction;
        private String _currentUser;
        private ICommand nav;


        public WithdrawCommand(String user, WithdrawViewModel withdrawViewModel, NavigationStore navigationStore, Func<ViewModelBase> createHomeViewModel)
        {
            _currentUser = user;
            _navigationStore = navigationStore;
            _createHomeViewModel = createHomeViewModel;
            _currentTransaction = TransactionService.Instance(null);
            withdrawViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public WithdrawCommand(String user, WithdrawMortgageViewModel withdrawMortgageViewModel, NavigationStore navigationStore, Func<ViewModelBase> createHomeViewModel)
        {
            _currentUser = user;
            _navigationStore = navigationStore;
            _createHomeViewModel = createHomeViewModel;
            _currentTransaction = TransactionService.Instance(null);
            withdrawMortgageViewModel.PropertyChanged += OnViewModelPropertyChanged;
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

                switch (typeSourceID)
                {
                    case "CH":
                        if (_currentTransaction.Transaction.Amount % 10 == 0 &&
                            _currentTransaction.Transaction.Amount >= 0.00m &&
                            _currentTransaction.Transaction.Amount <= 1000.00m)
                            {
                            var accountCSource = bomDb.CheckingAccounts.FirstOrDefault
                                (u => u.Account == _currentTransaction.Transaction.Source);

                            if (accountCSource != null)
                            {
                                if (accountCSource.UpdateBalance(-_currentTransaction.Transaction.Amount))
                                {
                                    bomDb.SaveChanges();
                                    _currentTransaction.Transaction.ClientId = _currentUser;
                                    RecordTransaction();
                                }
                                else
                                    Warning();
                            }
                        }
                        else
                            OtherWarning();
                        break;

                    case "SV":
                        if (_currentTransaction.Transaction.Amount % 10 == 0 &&
                            _currentTransaction.Transaction.Amount >= 0.00m &&
                            _currentTransaction.Transaction.Amount <= 1000.00m)
                        {
                            var accountSSource = bomDb.SavingAccounts.FirstOrDefault
                            (u => u.Account == _currentTransaction.Transaction.Source);

                            if (accountSSource != null)
                            {
                                if (accountSSource.UpdateBalance(-_currentTransaction.Transaction.Amount))
                                {
                                    bomDb.SaveChanges();
                                    _currentTransaction.Transaction.ClientId = _currentUser;
                                    RecordTransaction();
                                }
                                else
                                    Warning();
                            }
                        }else
                            OtherWarning();
                        break;
                    case "MG":
                        var accountMSource = bomDb.MortgageAccounts.FirstOrDefault
                            (u => u.Account == _currentTransaction.Transaction.Source);

                        if (accountMSource != null)
                        {
                            if (accountMSource.UpdateBalance(-_currentTransaction.Transaction.Amount))
                            {
                                bomDb.SaveChanges();
                                _currentTransaction.Transaction.ClientId = accountMSource.ClientID;
                                RecordTransaction();
                            }
                            else
                                Warning();
                        }
                        break;
                    default:
                        break;
                }
            nav = new NavigateCommand(_navigationStore, _createHomeViewModel);
            nav.Execute(null);
            _currentTransaction.Transaction = new TransactionData();
        }
        private void RecordTransaction()
        {
            _currentTransaction.Transaction.Out = true;
            _currentTransaction.Transaction.Destination = "CASH/OUT";
            var transaction = new Transaction();
            transaction.SetValues(_currentTransaction.Transaction);
            transaction.AddTransaction();
        }
        private void Warning()
        {
            MessageBox.Show("Insufficient balance");
        }
        private void OtherWarning()
        {
            MessageBox.Show("Withdrawal amount must be in multiples of 10, and be under $1000.");
            
        }
    }
}

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
    internal class TransferCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        private Func<ClientHomeViewModel> _createHomeViewModel;
        private TransactionService _currentTransaction;
        private String _currentUser;
        private ICommand nav;


        public TransferCommand(String user, TransferViewModel transferViewModel, NavigationStore navigationStore, Func<ClientHomeViewModel> createHomeViewModel)
        {
            _currentUser = user;
            _navigationStore = navigationStore;
            _createHomeViewModel = createHomeViewModel;
            _currentTransaction = TransactionService.Instance(null);
            transferViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public TransferCommand(String user, PayBillViewModel payBillViewModel, NavigationStore navigationStore, Func<ClientHomeViewModel> createHomeViewModel)
        {
            _currentUser = user;
            _navigationStore = navigationStore;
            _createHomeViewModel = createHomeViewModel;
            _currentTransaction = TransactionService.Instance(null);
            payBillViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TransferViewModel.Amount) ||
                e.PropertyName == nameof(TransferViewModel.Source) ||
                e.PropertyName == nameof(TransferViewModel.Recipient))
            {
                OnCanExecuteChanged();
            }
        }
        public override bool CanExecute(object parameter)
        {
            if (!String.IsNullOrEmpty(_currentTransaction.Transaction.Source) && !String.IsNullOrEmpty(_currentTransaction.Transaction.Destination) && _currentTransaction.Transaction.Amount > 0)
            {
                return true;
            }
            else
                return false;
        }

        public override void Execute(object parameter)
        {

            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                var typeDestinationID = _currentTransaction.Transaction.Destination.Substring(0, 2);
                var accountSource = bomDb.CheckingAccounts.FirstOrDefault
                    (u => u.Account == _currentTransaction.Transaction.Source);
                if (accountSource != null)
                {
                    if (accountSource.UpdateBalance(-_currentTransaction.Transaction.Amount))
                    {
                        bomDb.SaveChanges();
                        RecordTransaction();
                    }
                    else
                        Warning();
                }
            
                switch (typeDestinationID)
                {
                    case "CH":
                        var accountCSource = bomDb.CheckingAccounts.FirstOrDefault
                            (u => u.Account == _currentTransaction.Transaction.Destination);

                        if (accountCSource != null)
                        {
                            accountCSource.UpdateBalance(_currentTransaction.Transaction.Amount);
                            bomDb.SaveChanges();
                            _currentTransaction.Transaction.Out = false;
                        }
                        break;

                    case "SV":
                        var accountSSource = bomDb.SavingAccounts.FirstOrDefault
                            (u => u.Account == _currentTransaction.Transaction.Destination);

                        if (accountSSource != null)
                        {
                            accountSSource.UpdateBalance(_currentTransaction.Transaction.Amount);
                            bomDb.SaveChanges();
                            _currentTransaction.Transaction.Out = false;
                        }
                        break;
                    case "MG":
                        var accountMSource = bomDb.MortgageAccounts.FirstOrDefault
                            (u => u.Account == _currentTransaction.Transaction.Destination);

                        if (accountMSource != null)
                        {
                            accountMSource.UpdateBalance(_currentTransaction.Transaction.Amount);
                            bomDb.SaveChanges();
                            _currentTransaction.Transaction.Out = false;
                        }
                        break;
                    case "CR":
                        var accountCRSource = bomDb.CreditLineAccounts.FirstOrDefault
                            (u => u.Account == _currentTransaction.Transaction.Destination);

                        if (accountCRSource != null)
                        {
                            accountCRSource.UpdateBalance(-_currentTransaction.Transaction.Amount);
                            bomDb.SaveChanges();
                            _currentTransaction.Transaction.Out = false;
                        }
                        break;
                    default:
                        MessageBox.Show(_currentTransaction.Transaction.Destination);
                        _currentTransaction.Transaction.Out = true;
                        nav = new PayBillCommand(_currentTransaction.Transaction.Source);
                        nav.Execute(null);
                        break;
                }
                nav = new NavigateCommand(_navigationStore, _createHomeViewModel);
                nav.Execute(null);
                _currentTransaction.Transaction = new TransactionData();
            }
        }
        private void RecordTransaction()
        {
            _currentTransaction.Transaction.Out = true;
            _currentTransaction.Transaction.ClientId = _currentUser;
            var transaction = new Transaction();
            transaction.SetValues(_currentTransaction.Transaction);
            transaction.AddTransaction();
        }
        private void Warning()
        {
            MessageBox.Show("Insufficient balance");
        }
    }
}

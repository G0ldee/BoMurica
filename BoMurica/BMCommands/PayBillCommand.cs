using BoMurica.BMServices;
using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMCommands
{
    internal class PayBillCommand:CommandBase
    {
        private TransactionService _currentTransaction;

        public PayBillCommand(string source)
        {
            _currentTransaction = TransactionService.Instance(null);
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            using (BomDBEntities1 bomDb = new BomDBEntities1())
            {
                var accountSource = bomDb.CheckingAccounts.FirstOrDefault
                    (u => u.Account == _currentTransaction.Transaction.Source);
                if (accountSource != null)
                {
                    setTransaction();
                    if (accountSource.UpdateBalance(-_currentTransaction.Transaction.Amount))
                    {
                        bomDb.SaveChanges();
                        RecordTransaction();
                    }
                }
            }
        }
        private void RecordTransaction()
        {
            var transaction = new Transaction();
            transaction.SetValues(_currentTransaction.Transaction);
            transaction.AddTransaction();
        }
        private void setTransaction()
        {
            _currentTransaction.Transaction.Out = true;
            _currentTransaction.Transaction.Destination = "FEE";
            _currentTransaction.Transaction.Amount = 1.25m;
        }
    }
}

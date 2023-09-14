using BoMurica.BMModels;
using BoMurica.BMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoMurica.BMCommands
{
    internal class SetInterestCommand : CommandBase
    {
        private TransactionService _currentTransaction;
        private decimal _amount;
        private string _client;


        public SetInterestCommand() 
        {
            _currentTransaction = TransactionService.Instance(null);
            _currentTransaction.Transaction = new TransactionData();
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            {
                var option = parameter as String;
                if (option != null)
                {
                    switch (option)
                    {
                        case "Savings":
                            List<SavingAccount> savingsList = new List<SavingAccount>();
                            using (BomDBEntities1 bomDb = new BomDBEntities1())
                            {
                                savingsList = bomDb.SavingAccounts.ToList();
                            }
                            savingsList.ForEach(item =>
                            {
                                _client = item.GetID();
                                _amount = item.ApplyInterest();
                                RecordTransaction(item.Account);
                            });
                            MessageBox.Show("Interest Payed");
                            break;
                        case "Credit":
                            List<CreditLineAccount> creditList = new List<CreditLineAccount>();
                            using (BomDBEntities1 bomDb = new BomDBEntities1())
                            {
                                creditList = bomDb.CreditLineAccounts.ToList();
                            }
                            creditList.ForEach(item =>
                            {
                                _client = item.GetID();
                                _amount = item.ApplyInterest();
                                RecordTransaction(item.Account);
                            });
                            MessageBox.Show("Interest Charged");
                            break;
                            default: break;
                    }
                }
            }
        }
        private void RecordTransaction(String account)
        {
            _currentTransaction.Transaction.Out = false;
            _currentTransaction.Transaction.Source = "Bank";
            _currentTransaction.Transaction.Destination = account;
            _currentTransaction.Transaction.Amount = _amount;
            _currentTransaction.Transaction.ClientId = _client;
            var transaction = new Transaction();
            transaction.SetValues(_currentTransaction.Transaction);
            transaction.AddTransaction();
        }
    }
}

using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMViewModel
{
    internal class TransactionViewModel: ViewModelBase
    {
        private readonly Transaction _transaction; 

        public String TransactionId => _transaction.TransactionId.ToString();
        public string ClientID => _transaction.ClientID;
        public string AccountID => _transaction.AccountID;
        public string Amount => _transaction.Amount.ToString();
        public string Receiver => _transaction.Receiver;

        public TransactionViewModel(Transaction transaction)
        {
            _transaction = transaction;
        }
    }
}

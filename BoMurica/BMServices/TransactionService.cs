using BoMurica.BMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMServices
{
    internal class TransactionService
    {
        private TransactionService() { }

        private TransactionData _transacation;

        public TransactionData Transaction
        {
            get { return _transacation; }
            set { _transacation = value; }
        }

        private static TransactionService _instance;

        private static readonly object _lock = new object();

        public static TransactionService Instance(TransactionData transaction)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new TransactionService();
                        _instance.Transaction = transaction;
                    }
                }
            }
            return _instance;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMViewModel
{
    internal class SavingsAccountViewModel:ViewModelBase
    {
        private readonly SavingAccount _savingAccount;

        public string Account => _savingAccount.Account;
        public string ClientID => _savingAccount.ClientID;
        public decimal Balance => _savingAccount.Balance;

        public SavingsAccountViewModel(SavingAccount savingAccount)
        {
            _savingAccount = savingAccount;
        }
    }
}

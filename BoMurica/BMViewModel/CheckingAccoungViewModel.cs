using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMViewModel
{
    internal class CheckingAccoungViewModel:ViewModelBase
    {
        private readonly CheckingAccount _checkingAccount;

        public string Account => _checkingAccount.Account;
        public string ClientID => _checkingAccount.ClientID;
        public decimal Balance => _checkingAccount.Balance;

        public CheckingAccoungViewModel(CheckingAccount checkingAccount)
        {
            _checkingAccount = checkingAccount;
        }
    }
}

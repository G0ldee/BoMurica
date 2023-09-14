using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMViewModel
{
    internal class CreditLineViewModel:ViewModelBase
    {
        private readonly CreditLineAccount _creditLineAccount;

        public string Account => _creditLineAccount.Account;
        public string ClientID => _creditLineAccount.ClientID;
        public decimal Balance => _creditLineAccount.Balance;

        public CreditLineViewModel(CreditLineAccount creditLine)
        {
            _creditLineAccount = creditLine;
        }
    }
}

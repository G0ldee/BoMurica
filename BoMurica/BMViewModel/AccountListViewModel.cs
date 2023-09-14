using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMViewModel
{
    internal class AccountListViewModel:ViewModelBase
    {
        private readonly CheckingAccoungViewModel _checkingAccount;
        private readonly SavingsAccountViewModel _savingAccount;
        private readonly CreditLineViewModel _creditLineAccount;
        private readonly MortgageViewModel _mortgageAccount;

        public CheckingAccoungViewModel CheckingAccount => _checkingAccount;
        public SavingsAccountViewModel SavingsAccount => _savingAccount;
        public CreditLineViewModel CreditLineAccount => _creditLineAccount;
        public MortgageViewModel MortgageAccount => _mortgageAccount;

        public AccountListViewModel(CheckingAccoungViewModel checkingAccount, SavingsAccountViewModel savingAccount,
            CreditLineViewModel creditLine, MortgageViewModel mortgage)
        {
            _checkingAccount = checkingAccount;
            _savingAccount = savingAccount;
            _creditLineAccount = creditLine;
            _mortgageAccount = mortgage;
        }
    }
}

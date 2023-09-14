using BankOfMerica.BMView;
using BoMurica.BMServices;
using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoMurica.BMCommands
{
    internal class AccountSelectionCommand : CommandBase
    {
        private NewAccountViewModel _newAccountVM;
        public AccountSelectionCommand(NewAccountViewModel newAccountViewModel)
        {
            _newAccountVM = newAccountViewModel;
        }
        private NewAccountService _newAccount;

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _newAccount = NewAccountService.Instance(null);
            //_newAccount.Account.Type = parameter.ToString();
            _newAccountVM.Type = parameter.ToString();
            //MessageBox.Show(_newAccount.Account.Type);
        }
    }
}

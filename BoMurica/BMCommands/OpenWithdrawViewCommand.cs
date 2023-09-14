using BankOfMerica.BMView;
using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BoMurica.BMCommands
{
    internal class OpenWithdrawViewCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createWithdrawViewModel;
        private NavigateCommand open;

        public OpenWithdrawViewCommand(NavigationStore navigationStore,
            Func<WithdrawViewModel> createWithdrawViewModel)
        {
            _navigationStore = navigationStore;
            _createWithdrawViewModel = createWithdrawViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            open = new NavigateCommand(_navigationStore, _createWithdrawViewModel);
            open.Execute(null);
        }
    }
}

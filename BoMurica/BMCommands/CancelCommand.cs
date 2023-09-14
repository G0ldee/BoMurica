using BoMurica.BMModels;
using BoMurica.BMServices;
using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BoMurica.BMCommands
{
    internal class CancelCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        private Func<ViewModelBase> _createHomeViewModel;
        private TransactionService _currentTransaction;
        private ICommand nav;

        public CancelCommand(NavigationStore navigationStore, Func<ViewModelBase> createHomeViewModel)
        {
            _navigationStore = navigationStore;
            _createHomeViewModel = createHomeViewModel;
            _currentTransaction = TransactionService.Instance(null);
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _currentTransaction.Transaction = new TransactionData();
            nav = new NavigateCommand(_navigationStore, _createHomeViewModel);
            nav.Execute(null);
        }
    }
}

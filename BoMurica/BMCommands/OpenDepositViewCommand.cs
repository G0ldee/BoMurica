using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BoMurica.BMCommands
{
    internal class OpenDepositViewCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createDepositViewModel;
        private NavigateCommand open;

        public OpenDepositViewCommand(NavigationStore navigationStore,
            Func<DepositViewModel> createDepositViewModel)
        {
            _navigationStore = navigationStore;
            _createDepositViewModel = createDepositViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;

        }

        public override void Execute(object parameter)
        {
            open = new NavigateCommand(_navigationStore, _createDepositViewModel);
            open.Execute(null);
        }
    }
}

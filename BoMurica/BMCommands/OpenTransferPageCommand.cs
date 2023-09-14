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
    internal class OpenTransferPageCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createTransferViewModel;
        private NavigateCommand open;

        public OpenTransferPageCommand(NavigationStore navigationStore,
            Func<TransferViewModel> createTransferViewModel)
        {
            _navigationStore = navigationStore;
            _createTransferViewModel = createTransferViewModel;
        }


        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            open = new NavigateCommand(_navigationStore, _createTransferViewModel);
            open.Execute(null);
        }
    }
}

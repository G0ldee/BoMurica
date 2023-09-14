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
    internal class OpenPayBillPageCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createPayBillViewModel;
        private NavigateCommand open;

        public OpenPayBillPageCommand(NavigationStore navigationStore,
            Func<PayBillViewModel> createPayBillViewModel)
        {
            _navigationStore = navigationStore;
            _createPayBillViewModel = createPayBillViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;

        }

        public override void Execute(object parameter)
        {
            open = new NavigateCommand(_navigationStore, _createPayBillViewModel);
            open.Execute(null);
        }
    }
}

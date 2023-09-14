using BoMurica.BMCommands;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BoMurica.BMViewModel
{
    internal class AtmStateViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        Func<AdminHomeViewModel> _createHomeViewModel;
        private CurrentUserService _currentUser;
        public ICommand SignOutPageCommand { get; }
        public ICommand CancelCommand { get; }
        public AtmStateViewModel(NavigationStore _navigationStore, Func<AdminHomeViewModel> createHomeViewModel)
        {

            this._navigationStore = _navigationStore;
            _currentUser = CurrentUserService.Instance(null);
            _createHomeViewModel = createHomeViewModel;
            SignOutPageCommand = new NavigateCommand(_navigationStore, createHomeViewModel);
            CancelCommand = new NavigateCommand(_navigationStore, createHomeViewModel);
    }
    }
}

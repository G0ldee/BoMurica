using BankOfMerica.BMView;
using BoMurica.BMViewModel;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BoMurica.BMCommands
{
    internal class GenerateClientFormCommand : CommandBase
    {
        private NavigationStore navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public GenerateClientFormCommand(NavigationStore navigationStore, Func<NewClientViewModel> createNewClientViewModel)
        {
            this.navigationStore = navigationStore;
            this._createViewModel = createNewClientViewModel;
        }

        public override bool CanExecute(object parameter)
        {            
            return true;
        }

        public override void Execute(object sender)
        {
        }
    }
}

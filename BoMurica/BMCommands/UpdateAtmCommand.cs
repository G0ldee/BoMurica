using BoMurica.BMViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoMurica.BMCommands
{
    internal class UpdateAtmCommand : CommandBase
    {
        ATM atm = new ATM();

        public UpdateAtmCommand(AdminHomeViewModel adminHomeViewModel)
        {
            adminHomeViewModel.PropertyChanged += OnViewModelPropertyChanged;

        }
        public override bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                AtmTmpData tmpAtm = (AtmTmpData)parameter;

                using (BomDBEntities1 bomDb = new BomDBEntities1())
                {
                    atm = bomDb.ATMs.First();
                }
                Decimal tmpDeci = atm.Supply + tmpAtm.amount;
                if (atm.Active != tmpAtm.active || atm.Supply != tmpDeci && tmpAtm.amount != 0.00m)
                {
                    return true;
                }
            }
            return false;

        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AdminHomeViewModel.Amount) ||
                e.PropertyName == nameof(AdminHomeViewModel.Active))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {
            if (parameter != null)
            {
                AtmTmpData tmpAtm = (AtmTmpData)parameter;
                using (BomDBEntities1 bomDb = new BomDBEntities1())
                {
                    ATM atm = bomDb.ATMs.First();
                    atm.UpdateBalance(tmpAtm.amount);
                    bomDb.SaveChanges();
                    MessageBox.Show("Bueno");
                }
            }
        }
    }
}

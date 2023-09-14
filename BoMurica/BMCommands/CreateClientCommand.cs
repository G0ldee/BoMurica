using BoMurica.BMViewModel;
using BoMurica.Stores;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoMurica.BMCommands
{
    internal class CreateClientCommand : CommandBase
    {
        private ClientInfo client;
        public NavigateCommand CreateCommand;
        private NavigationStore _navigationStore;
        private Func<AdminHomeViewModel> _createViewModel;

        public CreateClientCommand(NewClientViewModel newClientViewModel,NavigationStore navigationStore, Func<BMViewModel.AdminHomeViewModel> createHomeViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createHomeViewModel;
            client = new ClientInfo();
            newClientViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NewClientViewModel.FirstName) ||
                e.PropertyName == nameof(NewClientViewModel.LastName) ||
                e.PropertyName == nameof(NewClientViewModel.Phone) ||
                e.PropertyName == nameof(NewClientViewModel.Email))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object newClient)
        {
            if (newClient != null)
            {
                client = newClient as ClientInfo;
                if (!String.IsNullOrEmpty(client.FirstName) &&
                    !String.IsNullOrEmpty(client.LastName) &&
                    !String.IsNullOrEmpty(client.Phone) &&
                    !String.IsNullOrEmpty(client.Email))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Execute(object parameter)
        {
             client = parameter as ClientInfo;

            int pass;
            pass = int.Parse(Interaction.InputBox("Ask client to type a password", "Client Password", "Password:"));
            if (pass != 0)
            {
                try
                {
                    client.SetClientId();
                    client.AddClient(pass);
                    MessageBox.Show("Client Added");
                    CreateCommand = new NavigateCommand(_navigationStore, _createViewModel);
                    CreateCommand.Execute(null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { }
            }
        }
    }
}

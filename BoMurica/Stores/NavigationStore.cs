using BoMurica.BMViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.Stores
{
    internal class NavigationStore
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentValueChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentValueChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}

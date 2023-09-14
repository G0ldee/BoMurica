using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BoMurica.BMCommands
{
    internal abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        protected void OnCanExecuteChanged() 
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

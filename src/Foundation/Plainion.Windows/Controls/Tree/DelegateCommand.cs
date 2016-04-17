using System;
using System.Windows.Input;

namespace Plainion.Windows.Controls.Tree
{
    /// <summary>
    /// Simple implementation of a DelegateCommand which allows easy callbacks to used DataContext
    /// </summary>
    class DelegateCommand : ICommand
    {
        private Action myDelegate;

        public DelegateCommand(Action action)
        {
            myDelegate = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            myDelegate();
        }
    }
}

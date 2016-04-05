using System;
using System.Windows.Input;

namespace Plainion.Windows.Controls.Tree
{
    class DelegateCommand : ICommand
    {
        private Action myDelegate;

        public DelegateCommand(Action @delegate)
        {
            myDelegate = @delegate;
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

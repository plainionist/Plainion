using System;
using System.Windows.Input;

namespace Plainion.Windows.Controls.Tree
{
    public class DelegateCommand : ICommand
    {
        public DelegateCommand(Action execute)
        {
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
        }

        internal void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }

    public class DelegateCommand<T> : DelegateCommand
    {
        public DelegateCommand(Action<T> execute)
            : base(() => execute(default(T)))
        {
        }

        public DelegateCommand(Action<T> execute, Func<bool> canExecute)
            : base(() => execute(default(T)), canExecute)
        {
        }
    }
}

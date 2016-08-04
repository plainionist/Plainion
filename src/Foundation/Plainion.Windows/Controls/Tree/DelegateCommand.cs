using System;
using System.Windows.Input;

namespace Plainion.Windows.Controls.Tree
{
    /// <summary>
    /// Simple implementation of a DelegateCommand which allows easy callbacks to used DataContext
    /// </summary>
    class DelegateCommand : ICommand
    {
        private readonly Action myDelegate;
        private readonly Func<bool> myCanExecuteDelegate;

        public DelegateCommand( Action execute)
            :this(execute, ()=>true)
        {
        }

        public DelegateCommand( Action execute, Func<bool> canExecute )
        {
            myDelegate = execute;
            myCanExecuteDelegate = canExecute;
        }

        public bool CanExecute( object parameter )
        {
            return myCanExecuteDelegate();
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

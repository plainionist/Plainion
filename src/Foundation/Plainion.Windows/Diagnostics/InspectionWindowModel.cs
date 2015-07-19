using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Plainion.Windows.Diagnostics
{
    class InspectionWindowModel : INotifyPropertyChanged
    {
        private class DelegateCommand : ICommand
        {
            private Action<object> myAction;

            public DelegateCommand( Action<object> action )
            {
                myAction = action;
            }

            public bool CanExecute( object parameter )
            {
                return true;
            }

#pragma warning disable 0067
            public event EventHandler CanExecuteChanged;
#pragma warning restore 0067

            public void Execute( object parameter )
            {
                myAction( parameter );
            }
        }

        public InspectionWindowModel()
        {
            RefreshCommand = new DelegateCommand( ignore => WpfStatics.CollectStatistics() );
        }

        public ObservableCollection<DiagnosticFinding> Findings
        {
            get { return WpfStatics.Findings; }
        }

        public ICommand RefreshCommand { get; private set; }

#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067
    }
}

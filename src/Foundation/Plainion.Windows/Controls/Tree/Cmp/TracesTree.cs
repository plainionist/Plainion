using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace Plainion.Windows.Controls.Tree.Cmp
{
    public class TracesTree : BindableBase
    {
        private ICollectionView myVisibleProcesses;
        private string myFilter;
        private IReadOnlyCollection<TraceProcessNode> myProcesses;

        public IReadOnlyCollection<TraceProcessNode> Processes
        {
            get { return myProcesses; }
            set
            {
                if( SetProperty( ref myProcesses, value ) )
                {
                    if( myProcesses != null )
                    {
                        foreach( var process in myProcesses )
                        {
                            PropertyChangedEventManager.AddHandler( process, OnProcessIsCheckedChanged, "IsChecked" );
                        }
                    }

                    myVisibleProcesses = null;
                    OnPropertyChanged( "VisibleProcesses" );
                }
            }
        }

        private void OnProcessIsCheckedChanged( object sender, PropertyChangedEventArgs e )
        {
            OnPropertyChanged( "IsChecked" );
        }

        public bool? IsChecked
        {
            get
            {
                if( myProcesses == null )
                {
                    return null;
                }

                if( Processes.All( p => p.IsChecked == true ) )
                {
                    return true;
                }

                if( Processes.All( p => p.IsChecked == false ) )
                {
                    return false;
                }

                return null;
            }
            set
            {
                foreach( var t in Processes )
                {
                    t.IsChecked = value.HasValue && value.Value;
                }

                OnPropertyChanged( "IsChecked" );
            }
        }

        public string Filter
        {
            get { return myFilter; }
            set
            {
                if( SetProperty( ref myFilter, value ) )
                {
                    foreach( var process in Processes )
                    {
                        process.ApplyFilter( myFilter );
                    }

                    VisibleProcesses.Refresh();
                }
            }
        }

        public ICollectionView VisibleProcesses
        {
            get
            {
                if( myVisibleProcesses == null && myProcesses != null )
                {
                    myVisibleProcesses = CollectionViewSource.GetDefaultView( Processes );
                    myVisibleProcesses.Filter = i => !( ( TraceProcessNode )i ).IsFilteredOut;

                    OnPropertyChanged( "VisibleProcesses" );
                }
                return myVisibleProcesses;
            }
        }
    }
}

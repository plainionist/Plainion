using System;

namespace Plainion.Windows.Controls.Tree.Cmp
{
    public class TraceThreadNode : NodeBase<TraceThread>
    {
        public TraceThreadNode( TraceThread model )
            : base( model )
        {
        }

        public string ThreadIdText
        {
            get { return Name == null ? string.Format( "Tid={0}", Model.ThreadId ) : string.Format( "(Tid={0})", Model.ThreadId ); }
        }

        protected override void OnModelPropertyChanged( string propertyName )
        {
            if( propertyName == "Name" )
            {
                OnPropertyChanged( "ThreadIdText" );
            }
            else if( propertyName == "IsSelected" )
            {
                OnPropertyChanged( "IsChecked" );
            }
        }

        public bool IsChecked
        {
            get { return Model.IsSelected; }
            set { Model.IsSelected = value; }
        }

        internal override void ApplyFilter( string filter )
        {
            if( string.IsNullOrWhiteSpace( filter ) )
            {
                IsFilteredOut = false;
            }
            else if( filter == "*" )
            {
                IsFilteredOut = Name == null;
            }
            else
            {
                IsFilteredOut = ( Name == null || !Name.Contains( filter, StringComparison.OrdinalIgnoreCase ) )
                    && !Model.ThreadId.ToString().Contains( filter );
            }
        }
    }
}

using System.ComponentModel;
using System.Windows.Input;

namespace Plainion.Windows.Controls.Tree.Cmp
{
    public abstract class NodeBase<T> : BindableBase where T : ModelBase
    {
        private bool myIsInEditMode;

        public NodeBase( T model )
        {
            Model = model;
            PropertyChangedEventManager.AddHandler( Model, OnModelPropertyChanged,string.Empty );

            EditNodeCommand = new DelegateCommand( () => IsInEditMode = true );
        }

        private void OnModelPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            OnModelPropertyChanged( e.PropertyName );

            if( e.PropertyName == "Name" )
            {
                OnPropertyChanged( "Name" );
            }
        }

        protected virtual void OnModelPropertyChanged( string propertyName )
        {
        }

        public T Model { get; private set; }

        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; }
        }

        public bool IsInEditMode
        {
            get { return myIsInEditMode; }
            set
            {
                if( Name == null && value == true )
                {
                    // we first need to set some dummy text so that the EditableTextBlock control becomes visible again
                    Name = "<empty>";
                }

                if( SetProperty( ref myIsInEditMode, value ) )
                {
                    if( !myIsInEditMode && Name == "<empty>" )
                    {
                        Name = null;
                    }
                }
            }
        }

        public ICommand EditNodeCommand { get; private set; }

        public bool IsFilteredOut { get; protected set; }

        internal abstract void ApplyFilter( string filter );
    }
}

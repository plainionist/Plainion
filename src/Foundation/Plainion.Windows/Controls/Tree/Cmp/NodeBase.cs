using System.Windows.Input;

namespace Plainion.Windows.Controls.Tree.Cmp
{
    public abstract class NodeBase : BindableBase 
    {
        private bool myIsInEditMode;
        private string myName;

        public NodeBase( )
        {
            EditNodeCommand = new DelegateCommand( () => IsInEditMode = true );
        }

        public string Name
        {
            get { return myName; }
            set { SetProperty(ref myName, value); }
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

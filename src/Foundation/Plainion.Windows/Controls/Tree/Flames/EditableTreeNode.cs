using System.Windows.Input;

namespace Plainion.Windows.Controls.Tree.Flames
{
    public class EditableTreeNode : BindableBase
    {
        private bool myIsInEditMode;

        public EditableTreeNode()
        {
            EditNodeCommand = new DelegateCommand( () => IsInEditMode = true );
        }

        public bool IsInEditMode
        {
            get { return myIsInEditMode; }
            set { SetProperty( ref myIsInEditMode, value ); }
        }

        public ICommand EditNodeCommand { get; private set; }
    }
}

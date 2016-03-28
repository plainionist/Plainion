using System.ComponentModel.Composition;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace Plainion.RI.Controls
{
    [Export]
    class EditableTextBlockViewModel : BindableBase
    {
        private string myName;
        private int mySize;
        private bool myIsInEditMode;

        public EditableTextBlockViewModel()
        {
            Name = "initial value";
            Size = Name.Length;

            EditCommand = new DelegateCommand( () => IsInEditMode = true );
        }

        public string Name
        {
            get { return myName; }
            set
            {
                if( SetProperty( ref myName, value ) )
                {
                    Size = Name.Length;
                }
            }
        }

        public int Size
        {
            get { return mySize; }
            set { SetProperty( ref mySize, value ); }
        }

        public ICommand EditCommand { get; private set; }

        public bool IsInEditMode
        {
            get { return myIsInEditMode; }
            set { SetProperty( ref myIsInEditMode, value ); }
        }
    }
}

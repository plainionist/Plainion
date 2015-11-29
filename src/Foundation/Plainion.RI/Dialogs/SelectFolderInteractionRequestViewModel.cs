using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Plainion.Prism.Interactivity.InteractionRequest;

namespace Plainion.RI.Dialogs
{
    [Export]
    class SelectFolderInteractionRequestViewModel : BindableBase
    {
        private string mySelectedFolder;

        public SelectFolderInteractionRequestViewModel()
        {
            SelectFolderCommand = new DelegateCommand( OnSelectFolder );
            SelectFolderRequest = new InteractionRequest<SelectFolderDialogNotification>();
        }

        public ICommand SelectFolderCommand { get; private set; }

        private void OnSelectFolder()
        {
            var notification = new SelectFolderDialogNotification();
            notification.Description = "Select some folder you like";

            SelectFolderRequest.Raise( notification, n =>
            {
                if( n.Confirmed )
                {
                    SelectedFolder = n.SelectedPath;
                }
            } );
        }

        public InteractionRequest<SelectFolderDialogNotification> SelectFolderRequest { get; private set; }

        public string SelectedFolder
        {
            get { return mySelectedFolder; }
            set { SetProperty( ref mySelectedFolder, value ); }
        }
    }
}

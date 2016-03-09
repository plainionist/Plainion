using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Plainion.RI.InteractionRequests.Dialogs;
using Prism.Interactivity.InteractionRequest;

namespace Plainion.RI.InteractionRequests
{
    [Export]
    class ComplexCustomViewViewModel : BindableBase
    {
        [ImportingConstructor]
        public ComplexCustomViewViewModel( Model model )
        {
            Model = model;

            ShowConfirmationCommand = new DelegateCommand( OnShowConfirmation );
            ConfirmationRequest = new InteractionRequest<INotification>();
        }

        public Model Model { get; private set; }

        public ICommand ShowConfirmationCommand { get; private set; }

        private void OnShowConfirmation()
        {
            var notification = new Notification();
            notification.Title = "Really?";

            // we create ViewModel with new here for simplicity. Of course we could also import it into e.g. private field 
            // in order to let MEF resolve all dependencies
            notification.Content = new ComplexDialogModel( Model );

            ConfirmationRequest.Raise( notification, n => { } );
        }

        public InteractionRequest<INotification> ConfirmationRequest { get; private set; }
    }
}

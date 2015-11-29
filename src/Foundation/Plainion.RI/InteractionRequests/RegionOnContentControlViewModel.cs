using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Plainion.RI.InteractionRequests.Dialogs;

namespace Plainion.RI.InteractionRequests
{
    /// <summary>
    /// This sample defines a region on a ContentControl inside PopupWindowAction.WindowContent. This way the requesting
    /// viewmodel doesnt need to know anything about the concreate view/model.
    /// <para>
    /// This approach works well for complex dialogs like "Settings" dialogs.
    /// This approach does NOT support IInteractionRequestAware for the dialog view model because PopupWindowAction only checks value of 
    /// WindowContent property and its DataContext (Dialog is child of ContentControl).
    /// </para>
    /// </summary>
    [Export]
    class RegionOnContentControlViewModel : BindableBase
    {
        public RegionOnContentControlViewModel()
        {
            // of course in real application we would NOT create the model here but import it from MEF
            Model = new Model();

            ShowConfirmationCommand = new DelegateCommand( OnShowConfirmation );
            ConfirmationRequest = new InteractionRequest<INotification>();
        }

        public Model Model { get; private set; }

        public ICommand ShowConfirmationCommand { get; private set; }

        private void OnShowConfirmation()
        {
            var notification = new Notification();
            notification.Title = "Really?";

            // view imported via region, viewmodel imported by view directly

            ConfirmationRequest.Raise( notification, n => { } );
        }

        public InteractionRequest<INotification> ConfirmationRequest { get; private set; }
    }
}

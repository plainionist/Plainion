using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Plainion.RI.InteractionRequests.Dialogs;

namespace Plainion.RI.InteractionRequests
{
    /// <summary>
    /// This sample defines a region on a PopupWindowContentControl inside PopupWindowAction.WindowContent. This way the requesting
    /// viewmodel doesnt need to know anything about the concreate view/model.
    /// <para>
    /// This approach works well for complex dialogs like "Settings" dialogs.
    /// This approach supports view importing there viewmodel directly AND supports IInteractionRequestAware for the viewmodel.
    /// </para>
    /// </summary>
    [Export]
    class RegionOnPopupWindowContentControlViewModel : BindableBase
    {
        [ImportingConstructor]
        public RegionOnPopupWindowContentControlViewModel( Model model )
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

            // view imported via region, viewmodel imported by view directly

            ConfirmationRequest.Raise( notification, n => { } );
        }

        public InteractionRequest<INotification> ConfirmationRequest { get; private set; }
    }
}

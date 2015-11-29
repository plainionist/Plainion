using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Plainion.RI.InteractionRequests.Dialogs;

namespace Plainion.RI.InteractionRequests
{
    /// <summary>
    /// This sample defines a region on the PopupWindowAction directly. This way the requesting
    /// viewmodel doesnt need to know anything about the concreate view/model.
    /// <para>
    /// This approach only works IF PopupWindowActionRegionAdapter is registers which requires KeepAliveDelayedRegionCreationBehavior to be 
    /// in the CompositionContainer.
    /// This approach supports view importing there viewmodel directly AND supports IInteractionRequestAware for the viewmodel.
    /// </para>
    /// </summary>
    [Export]
    class RegionOnPopupWindowActionViewModel : BindableBase
    {
        [ImportingConstructor]
        public RegionOnPopupWindowActionViewModel( Model model )
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
            // BUT we have to call this here to trigger import of view into region. Seems that as PopupWindowAction is no 
            // FrameworkElement Prism misses some trigger to create and update the region for the PopupWindowAction.
            RegionManager.UpdateRegions();

            ConfirmationRequest.Raise( notification, n => { } );
        }

        public InteractionRequest<INotification> ConfirmationRequest { get; private set; }
    }
}

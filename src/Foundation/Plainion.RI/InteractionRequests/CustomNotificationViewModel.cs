using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;

namespace Plainion.RI.InteractionRequests
{
    /// <summary>
    /// This sample provides a custom PopupWindowAction.WindowContent (directly in Xaml)and a custom 
    /// INotification. This notification serves as DataContext for the custom view.
    /// <para>
    /// This approach works well for simple notifications and confirmations with minimal customized view or logic.
    /// In these simple cases it is fine to create the notification with new in the requesting viewmodel and just provide
    /// additional parameters directly. One drawback of this approach is that the notification object has two responsibilities:
    /// it is used by the requestion viewmodel to pass "parameters" to the popup window and acts as DataContext for the custom view of the popup window.
    /// </para>
    /// </summary>
    [Export]
    class CustomNotificationViewModel : BindableBase
    {
        private string myResponse;

        public CustomNotificationViewModel()
        {
            ShowConfirmationCommand = new DelegateCommand( OnShowConfirmation );
            ConfirmationRequest = new InteractionRequest<YesNoCancelNotification>();
        }

        public ICommand ShowConfirmationCommand { get; private set; }

        private void OnShowConfirmation()
        {
            var confirmation = new YesNoCancelNotification( "Really?" );
            confirmation.Title = "Really?";

            ConfirmationRequest.Raise( confirmation, c => Response = c.Response.ToString() );
        }

        public InteractionRequest<YesNoCancelNotification> ConfirmationRequest { get; private set; }

        public string Response
        {
            get { return myResponse; }
            set { SetProperty( ref myResponse, value ); }
        }
    }

    public class YesNoCancelNotification : Notification, IInteractionRequestAware
    {
        public enum ResponseType
        {
            Cancel,
            Yes,
            No
        }

        public YesNoCancelNotification( string question )
        {
            Question = question;

            YesCommand = new DelegateCommand( () => OnConfirmed( ResponseType.Yes ) );
            NoCommand = new DelegateCommand( () => OnConfirmed( ResponseType.No ) );
            CancelCommand = new DelegateCommand( () => OnConfirmed( ResponseType.Cancel ) );

            Response = ResponseType.Cancel;
        }

        public string Question { get; private set; }

        public ResponseType Response { get; private set; }

        public ICommand YesCommand { get; private set; }

        public ICommand NoCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        private void OnConfirmed( ResponseType response )
        {
            Response = response;
            FinishInteraction();
        }

        public Action FinishInteraction { get; set; }

        public INotification Notification { get; set; }
    }
}

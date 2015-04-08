using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Plainion.Controls.Interactivity.InteractionRequest
{
    public static class InteractionRequestExtensions
    {
        public static void TrySetConfirmed( this INotification notification, bool confirmed )
        {
            var confirmation = notification as IConfirmation;
            if( confirmation != null )
            {
                confirmation.Confirmed = confirmed;
            }
        }
    }
}

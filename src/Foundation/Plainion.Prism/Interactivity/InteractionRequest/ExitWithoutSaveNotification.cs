using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Plainion.Prism.Interactivity.InteractionRequest
{
    public class ExitWithoutSaveNotification : Notification, IInteractionRequestAware
    {
        public enum ResponseType
        {
            Cancel,
            Yes,
            No
        }

        public ExitWithoutSaveNotification()
        {
            YesCommand = new DelegateCommand( () => OnConfirmed( ResponseType.Yes ) );
            NoCommand = new DelegateCommand( () => OnConfirmed( ResponseType.No ) );
            CancelCommand = new DelegateCommand( () => OnConfirmed( ResponseType.Cancel ) );

            Response = ResponseType.Cancel;
        }

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

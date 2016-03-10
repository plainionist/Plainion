using System;
using System.Windows;
using System.Windows.Controls;
using Prism.Interactivity.InteractionRequest;

namespace Plainion.Prism.Interactivity
{
    /// <summary>
    /// Custom ContentControl which can be used as placeholder in PopupWindowActions WindowContent to define a region.
    /// This control supports IInteractionRequestAware by view/viewmodel of the region content.
    /// </summary>
    public class PopupWindowContentControl : ContentControl, IInteractionRequestAware
    {
        private Action myFinishInteration;
        private INotification myNotification;

        public Action FinishInteraction
        {
            get { return myFinishInteration; }
            set
            {
                if( myFinishInteration == value )
                {
                    return;
                }

                myFinishInteration = value;

                UpdateInteractionRequestEnvironmentOnContent();
            }
        }

        private void UpdateInteractionRequestEnvironmentOnContent()
        {
            if( Content == null )
            {
                return;
            }

            TryUpdateInteractionRequestEnvironmentOn( Content as IInteractionRequestAware );

            var frameworkElement = Content as FrameworkElement;
            if( frameworkElement == null )
            {
                return;
            }

            TryUpdateInteractionRequestEnvironmentOn( frameworkElement.DataContext as IInteractionRequestAware );
        }

        private void TryUpdateInteractionRequestEnvironmentOn( IInteractionRequestAware interactionRequestAware )
        {
            if( interactionRequestAware == null )
            {
                return;
            }

            interactionRequestAware.FinishInteraction = myFinishInteration;
            interactionRequestAware.Notification = myNotification;
        }

        public INotification Notification
        {
            get { return myNotification; }
            set
            {
                if( myNotification == value )
                {
                    return;
                }

                myNotification = value;

                UpdateInteractionRequestEnvironmentOnContent();
            }
        }

        protected override void OnContentChanged( object oldContent, object newContent )
        {
            base.OnContentChanged( oldContent, newContent );

            UpdateInteractionRequestEnvironmentOnContent();
        }
    }
}

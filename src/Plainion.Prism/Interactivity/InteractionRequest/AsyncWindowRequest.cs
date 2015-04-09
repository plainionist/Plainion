using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Plainion.Prism.Interactivity.InteractionRequest
{
    public class AsyncWindowRequest : IAsyncWindowRequest
    {
        private Type myWindowContent;

        public AsyncWindowRequest( Type windowContent )
        {
            myWindowContent = windowContent;
        }

        public Task Raise( INotification notification )
        {
            return Tasks.Tasks.StartSTATask( () =>
                {
                    var window = new Window();

                    window.Title = notification.Title;

                    var dialog = notification as IDialog;
                    if( dialog != null )
                    {
                        if( dialog.Width.HasValue )
                        {
                            window.Width = dialog.Width.Value;
                        }

                        if( dialog.Height.HasValue )
                        {
                            window.Height = dialog.Height.Value;
                        }
                    }

                    window.Content = ( FrameworkElement )Activator.CreateInstance( myWindowContent );
                    window.DataContext = notification.Content;

                    var requestAware = notification.Content as IInteractionRequestAware;
                    if( requestAware != null )
                    {
                        requestAware.Notification = notification;
                        requestAware.FinishInteraction = () => window.Close();
                    }

                    window.Closed += ( s, e ) => ( ( Window )s ).Dispatcher.InvokeShutdown();

                    window.Show();

                    Dispatcher.Run();
                } );
        }
    }
}

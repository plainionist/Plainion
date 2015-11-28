using System.Windows;
using System.Windows.Threading;

namespace Plainion.RI
{
    public partial class App : Application
    {
        protected override void OnStartup( StartupEventArgs e )
        {
            DispatcherUnhandledException += OnUnhandledException;

            base.OnStartup( e );

            new Bootstrapper().Run();
        }

        private void OnUnhandledException( object sender, DispatcherUnhandledExceptionEventArgs e )
        {
            MessageBox.Show( e.Exception.ToString(), "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error );

            e.Handled = true;
        }
    }
}

using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Plainion.Prism.Events;
using Plainion.Windows;
using Prism.Events;
using Prism.Mef;

namespace Plainion.AppFw.Wpf
{
    public class BootstrapperBase<TShell> : MefBootstrapper where TShell : Window
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<TShell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override CompositionContainer CreateContainer()
        {
            return new CompositionContainer( AggregateCatalog, CompositionOptions.DisableSilentRejection );
        }

        public override void Run( bool runWithDefaultConfiguration )
        {
            new UnhandledExceptionHook( Application.Current );

            base.Run( runWithDefaultConfiguration );

            Application.Current.Exit += OnShutdown;
            
            Container.GetExportedValue<IEventAggregator>().GetEvent<ApplicationReadyEvent>().Publish( null );
        }

        protected virtual void OnShutdown( object sender, ExitEventArgs e )
        {
            Container.GetExportedValue<IEventAggregator>().GetEvent<ApplicationShutdownEvent>().Publish( null );

            Container.Dispose();
        }
    }
}

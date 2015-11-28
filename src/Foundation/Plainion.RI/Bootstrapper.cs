using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Regions;
using Plainion.Prism.Regions;

namespace Plainion.RI
{
    class Bootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = ( Window )Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            AggregateCatalog.Catalogs.Add( new AssemblyCatalog( GetType().Assembly ) );
            AggregateCatalog.Catalogs.Add( new AssemblyCatalog( typeof( StackPanelRegionAdapter ).Assembly ) );
        }

        protected override CompositionContainer CreateContainer()
        {
            return new CompositionContainer( AggregateCatalog, CompositionOptions.DisableSilentRejection );
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();

            mappings.RegisterMapping( typeof( StackPanel ), Container.GetExportedValue<StackPanelRegionAdapter>() );

            return mappings;
        }

        public override void Run( bool runWithDefaultConfiguration )
        {
            base.Run( runWithDefaultConfiguration );

            Application.Current.Exit += OnShutdown;
        }

        protected virtual void OnShutdown( object sender, ExitEventArgs e )
        {
            Container.Dispose();
        }
    }
}

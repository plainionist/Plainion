using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Windows.Controls;
using Plainion.Logging;
using Prism.Interactivity;
using Prism.Mef;
using Prism.Regions;

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

    [Export( typeof( StackPanelRegionAdapter ) )]
    public class StackPanelRegionAdapter : RegionAdapterBase<StackPanel>, IRegionAdapter
    {
        [ImportingConstructor]
        public StackPanelRegionAdapter( IRegionBehaviorFactory factory )
            : base( factory )
        {
        }

        protected override void Adapt( IRegion region, StackPanel regionTarget )
        {
            region.Views.CollectionChanged += ( s, e ) =>
            {
                if( e.Action == NotifyCollectionChangedAction.Add )
                {
                    foreach( FrameworkElement element in e.NewItems )
                    {
                        regionTarget.Children.Add( element );
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }

}

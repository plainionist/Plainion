using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Plainion.Prism.Regions
{
    [Export( typeof( StackPanelRegionAdapter ) )]
    public class StackPanelRegionAdapter : RegionAdapterBase<StackPanel>
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

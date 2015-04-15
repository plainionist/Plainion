using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Interactivity;
using Microsoft.Practices.Prism.Regions;

namespace Plainion.Prism.Interactivity
{
    [Export( typeof( PopupWindowActionRegionAdapter ) )]
    public class PopupWindowActionRegionAdapter : RegionAdapterBase<PopupWindowAction>
    {
        [ImportingConstructor]
        public PopupWindowActionRegionAdapter( IRegionBehaviorFactory factory )
            : base( factory )
        {
        }

        protected override void Adapt( IRegion region, PopupWindowAction regionTarget )
        {
            region.Views.CollectionChanged += ( s, e ) =>
                {
                    if( e.Action == NotifyCollectionChangedAction.Add )
                    {
                        foreach( FrameworkElement element in e.NewItems )
                        {
                            regionTarget.WindowContent = element;
                        }
                    }
                };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}

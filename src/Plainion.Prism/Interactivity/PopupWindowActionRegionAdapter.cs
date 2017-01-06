using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Windows;
using Prism.Interactivity;
using Prism.Regions;

namespace Plainion.Prism.Interactivity
{
    /// <summary>
    /// Allows defining regions on PopupWindowAction instances.
    /// ATTENTION: make sure you configured "KeepAliveDelayedRegionCreationBehavior" as well otherwise sporadically 
    /// your region will not get updated. See API doc of KeepAliveDelayedRegionCreationBehavior for more details.
    /// </summary>
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

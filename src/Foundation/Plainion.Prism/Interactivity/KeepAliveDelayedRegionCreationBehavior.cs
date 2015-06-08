using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;

namespace Plainion.Prism.Interactivity
{
    /// <summary>
    /// This DelayedRegionCreationBehavior is required if you want to put regions to non-FrameworkElement objects
    /// (like PopupWindowAction). In this case the instance of the DelayedRegionCreationBehavior is only referenced by
    /// RegionManager.UpdatingRegions event which internally uses WeakReferences. This means if GC catched you before 
    /// RegionManager.UpdateRegions() was called your region will never be updated.
    /// </summary>
    [Export( typeof( DelayedRegionCreationBehavior ) )]
    [PartCreationPolicy( CreationPolicy.NonShared )]
    public class KeepAliveDelayedRegionCreationBehavior : DelayedRegionCreationBehavior
    {
        private static List<KeepAliveDelayedRegionCreationBehavior> myKeepAlives = new List<KeepAliveDelayedRegionCreationBehavior>();

        [ImportingConstructor]
        public KeepAliveDelayedRegionCreationBehavior( RegionAdapterMappings regionAdapterMappings )
            : base( regionAdapterMappings )
        {
            // add a strong reference to a static list
            myKeepAlives.Add( this );
            GC.Collect( 2, GCCollectionMode.Forced );
        }

        protected override IRegion CreateRegion( DependencyObject targetElement, string regionName )
        {
            // remove myself because region is now created - job done
            myKeepAlives.Remove( this );

            return base.CreateRegion( targetElement, regionName );
        }
    }
}

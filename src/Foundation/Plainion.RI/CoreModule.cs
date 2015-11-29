using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Plainion.RI.Dialogs;
using Plainion.RI.InteractionRequests;

namespace Plainion.RI
{
    [ModuleExport( typeof( CoreModule ) )]
    class CoreModule : IModule
    {
        [Import]
        public IRegionManager RegionManager { get; private set; }

        public void Initialize()
        {
            RegionManager.RegisterViewWithRegion( RegionNames.Dialogs, typeof( SelectFolderDialogView ) );

            RegionManager.RegisterViewWithRegion( RegionNames.InteractionRequests, typeof( DefaultConfirmationView ) );
            RegionManager.RegisterViewWithRegion( RegionNames.InteractionRequests, typeof( CustomNotificationView ) );
        }
    }
}

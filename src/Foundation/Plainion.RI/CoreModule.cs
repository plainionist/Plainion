using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Plainion.RI.Dialogs;
using Plainion.RI.InteractionRequests;
using Plainion.RI.InteractionRequests.Dialogs;
using Plainion.RI.NotePad;

namespace Plainion.RI
{
    [ModuleExport(typeof(CoreModule))]
    class CoreModule : IModule
    {
        [Import]
        public IRegionManager RegionManager { get; private set; }

        public void Initialize()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.Dialogs, typeof(SelectFolderDialogView));
            RegionManager.RegisterViewWithRegion(RegionNames.Dialogs, typeof(SelectFolderInteractionRequestView));

            RegionManager.RegisterViewWithRegion(RegionNames.InteractionRequests, typeof(DefaultWindowWithViewAsContentView));
            RegionManager.RegisterViewWithRegion(RegionNames.InteractionRequests, typeof(DefaultWindowWithViewModelAsContentView));
            RegionManager.RegisterViewWithRegion(RegionNames.InteractionRequests, typeof(CustomNotificationView));
            RegionManager.RegisterViewWithRegion(RegionNames.InteractionRequests, typeof(ComplexCustomViewView));
            RegionManager.RegisterViewWithRegion(RegionNames.InteractionRequests, typeof(RegionOnContentControlView));
            RegionManager.RegisterViewWithRegion(RegionNames.InteractionRequests, typeof(RegionOnPopupWindowActionView));
            RegionManager.RegisterViewWithRegion(RegionNames.InteractionRequests, typeof(RegionOnPopupWindowContentControlView));
            RegionManager.RegisterViewWithRegion(RegionNames.InteractionRequests, typeof(RegionWithPopupWindowActionExtensionsView));

            RegionManager.RegisterViewWithRegion("RegionOnContentControlView", typeof(ComplexDialog));
            RegionManager.RegisterViewWithRegion("RegionOnPopupWindowActionView", typeof(ComplexDialog));
            RegionManager.RegisterViewWithRegion("RegionOnPopupWindowContentControlView", typeof(ComplexDialog));
            RegionManager.RegisterViewWithRegion("RegionWithPopupWindowActionExtensionsView", typeof(ComplexDialog));

            RegionManager.RegisterViewWithRegion(RegionNames.NotePad, typeof(NotePadView));
        }
    }
}

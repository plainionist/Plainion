using System.ComponentModel.Composition;
using Plainion.RI.Controls;
using Plainion.RI.Dialogs;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

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

            RegionManager.RegisterViewWithRegion( RegionNames.Controls, typeof( EditableTextBlockView ) );
            RegionManager.RegisterViewWithRegion( RegionNames.Controls, typeof( TreeEditorView ) );
            RegionManager.RegisterViewWithRegion( RegionNames.Controls, typeof( NotePadView ) );
            RegionManager.RegisterViewWithRegion( RegionNames.Controls, typeof( NoteBookView ) );
        }
    }
}

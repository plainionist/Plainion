# Plainion.Prism

## Prism.Interactivity.ExitWithoutSaveView
ExitWithoutSaveView

### Methods

#### InitializeComponent
InitializeComponent

## Prism.Interactivity.InteractionRequest.FileDialogNotificationBase
Notification base class for Microsoft.Win32.FileDialog.

### Properties

#### Filter


> *See: P:Microsoft.Win32.FileDialog.Filer*

#### FileName


> *See: P:Microsoft.Win32.FileDialog.FileName*

#### AddExtension


> *See: P:Microsoft.Win32.FileDialog.AddExtension*

#### CheckFileExists


> *See: P:Microsoft.Win32.FileDialog.CheckFileExists*

#### CheckPathExists


> *See: P:Microsoft.Win32.FileDialog.CheckPathExists*

#### CustomPlaces


> *See: P:Microsoft.Win32.FileDialog.CustomPlaces*

#### DefaultExt


> *See: P:Microsoft.Win32.FileDialog.DefaultExt*

#### DereferenceLinks


> *See: P:Microsoft.Win32.FileDialog.DereferenceLinks*

#### FileNames


> *See: P:Microsoft.Win32.FileDialog.FileNames*

#### FilterIndex


> *See: P:Microsoft.Win32.FileDialog.FilterIndex*

#### InitialDirectory


> *See: P:Microsoft.Win32.FileDialog.InitialDirectory*

#### SafeFileName


> *See: P:Microsoft.Win32.FileDialog.SafeFileName*

#### SafeFileNames


> *See: P:Microsoft.Win32.FileDialog.SafeFileNames*

#### ValidateNames


> *See: P:Microsoft.Win32.FileDialog.ValidateNames*

#### RestoreDirectory


> *See: P:Microsoft.Win32.FileDialog.RestoreDirectory*

## Prism.Interactivity.KeepAliveDelayedRegionCreationBehavior
This DelayedRegionCreationBehavior is required if you want to put regions to non-FrameworkElement objects (like PopupWindowAction). In this case the instance of the DelayedRegionCreationBehavior is only referenced by RegionManager.UpdatingRegions event which internally uses WeakReferences. This means if GC catched you before RegionManager.UpdateRegions() was called your region will never be updated.

## Prism.Interactivity.PopupWindowActionRegionAdapter
Allows defining regions on PopupWindowAction instances. ATTENTION: make sure you configured "KeepAliveDelayedRegionCreationBehavior" as well otherwise sporadically your region will not get updated. See API doc of KeepAliveDelayedRegionCreationBehavior for more details.

## Prism.Interactivity.PopupWindowContentControl
Custom ContentControl which can be used as placeholder in PopupWindowActions WindowContent to define a region. This control supports IInteractionRequestAware by view/viewmodel of the region content.

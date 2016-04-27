# Plainion.Windows

## Windows.Controls.AdornedControl

> ### Remarks

> Initial version taken from: - http://www.codeproject.com/Articles/57984/WPF-Loading-Wait-Adorner - http://www.codeproject.com/Articles/54472/Defining-WPF-Adorners-in-XAML

## Windows.Controls.AdornerPlacement
Specifies the placement of the adorner in related to the adorned control.

> ### Remarks

> Initial version taken from: - http://www.codeproject.com/Articles/57984/WPF-Loading-Wait-Adorner - http://www.codeproject.com/Articles/54472/Defining-WPF-Adorners-in-XAML

## Windows.Controls.DocumentNavigationPane
DocumentNavigationPane

### Methods

#### InitializeComponent
InitializeComponent

## Windows.Controls.NoteBook
NoteBook

### Methods

#### InitializeComponent
InitializeComponent

## Windows.Controls.NotePad
NotePad

### Methods

#### InitializeComponent
InitializeComponent

## Windows.Controls.SelectFolderDialog
Provides WPF wrapper for System.Windows.Forms.FolderBrowserDialog. It derives from in order to support same handling as and .

### Properties

#### Description

Gets or sets the descriptive text displayed above the tree view control in the dialog box.

#### RootFolder

Gets or sets the root folder where the browsing starts from.

#### SelectedPath

Gets or sets the path selected by the user.

#### ShowNewFolderButton

Gets or sets a value indicating whether the New Folder button appears in the folder browser dialog box.

### Methods

#### Reset
Resets properties to their default values.

## Windows.Controls.Tree.DelegateCommand
Simple implementation of a DelegateCommand which allows easy callbacks to used DataContext

## Windows.Controls.Tree.DragDropBehavior
Implements DragDrop in the tree. Can be used by users of the TreeEditor to handle Drag and Drop commands. Pre-conditions: - INode.Parent must be writable - INode.Children must be of type ObservableCollection{T}

## Windows.Controls.Tree.ExtendedTreeView
Used by the to handle certain aspects which are difficult to handle in the Xaml.

## Windows.Controls.Tree.IDragDropSupport
Can be implemented by implementations to control drag and drop allowence.

## Windows.Controls.Tree.INode
Required interface of nodes in .

## Windows.Controls.Tree.NodeDropRequest
Send as parameter with the to specify the requested DragDrop action.

## Windows.Controls.Tree.NodeState
The "state" master is always the actual DataContext (the implementation of INode). Only for state which is not represented by DataContext this class here is the master

## Windows.Controls.Tree.StateContainer
used to store additional state to the actual INode model. we cannot store state in NodeItem directly as those instances lifecylced ItemContainerGenerator. esp. with virtualization enabled those items might be created on demand and destroyed frequently.

## Windows.Controls.Tree.TreeEditor
TreeEditor

### Fields

#### DropCommandProperty

Parameter will be of type .

### Methods

#### InitializeComponent
InitializeComponent

## Windows.Controls.Tree.TreeEditorCommands
Defaults for the commands.

## Windows.Diagnostics.InspectionWindow
InspectionWindow

### Methods

#### InitializeComponent
InitializeComponent

## Windows.IsEmptyConverter
Returns true if the given value is null, an empty string or an empty collection, false otherwise.

## Windows.Controls.FrameworkElementAdorner
This class is an adorner that allows a FrameworkElement derived class to adorn another FrameworkElement.

> ### Remarks

> Initial version taken from: http://www.codeproject.com/Articles/57984/WPF-Loading-Wait-Adorner which was inspired by: http://www.codeproject.com/KB/WPF/WPFJoshSmith.aspx

### Methods

#### DetermineX
Determine the X coordinate of the child.

## Windows.Controls.CircularProgressIndicator
CircularProgressIndicator

> ### Remarks

> Initial version taken from: http://www.codeproject.com/Articles/57984/WPF-Loading-Wait-Adorner

### Methods

#### InitializeComponent
InitializeComponent

## Windows.MultiStyleExtension
Markup extension to merge all Styles given via ResourceKeys into single Style instance.

> ### Remarks

> Initial version taken from: http://web.archive.org/web/20101125040337/http://bea.stollnitz.com/blog/?p=384

### Properties

#### ResourceKeys

Space separated list of resource keys

### Methods

#### ProvideValue(System.IServiceProvider)
Returns a style that merges all styles with the keys specified by ResourceKeys property.

## Windows.Controls.SearchTextBox
Initial version comes from: http://davidowens.wordpress.com/2009/02/18/wpf-search-text-box/

## Windows.Interactivity.Triggers
Public collection of TriggerBase - Windows collections have no public ctor

## Windows.Interactivity.TriggersExtension
Extension to allow adding triggers via Styles

## Windows.InputBindingsExtension
Extension to allow adding InputBindings via Styles

## Windows.BindableProperty
References a property which provides change notifications

## Windows.UnhandledExceptionHook
Encapsulates handling of unhandled exceptions by popping up a message box on occurence.

## GeneratedInternalTypeHelper
GeneratedInternalTypeHelper

### Methods

#### CreateInstance(System.Type,System.Globalization.CultureInfo)
CreateInstance

#### GetPropertyValue(System.Reflection.PropertyInfo,System.Object,System.Globalization.CultureInfo)
GetPropertyValue

#### SetPropertyValue(System.Reflection.PropertyInfo,System.Object,System.Object,System.Globalization.CultureInfo)
SetPropertyValue

#### CreateDelegate(System.Type,System.Object,System.String)
CreateDelegate

#### AddEventHandler(System.Reflection.EventInfo,System.Object,System.Delegate)
AddEventHandler

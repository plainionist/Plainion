
This namespace provides a simple TreeEditor control and related classes.

# Features

- INode is the only required contract for node implementations
- Filtering of nodes
- Drag & Drop of nodes with reusable default behavior (see DragDropBehavior class)
  - can be controlled via IDragDropSupport and DropCommand
- In-place edit of Node text
  - Automatically supported if the Text property is bound with TwoWay mode
- Creation of new nodes via ContextMenu
  - Automatically available if some Command is bound to CreateChildCommand property
- Deletion of nodes via ContextMenu
  - Automatically available if some Command is bound to DeleteCommand property

# Usage

- just implement INode and bind the root of your tree to the Root property of the TreeEditor
- for a fully working example see Plainion.RI/Controls/TreeEditorView

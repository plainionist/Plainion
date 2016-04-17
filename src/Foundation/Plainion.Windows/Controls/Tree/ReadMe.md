
This namespace provides a simple TreeEditor control and related classes.

# Features

- INode is the only required contract for node implementations
- Filtering of nodes
- Drag & Drop of nodes
  - can be controlled via IDragDropSupport and DropCommand
- In-place edit of Node text
- Creation of new nodes via ContextMenu
- Deletion of nodes via ContextMenu

# Usage

- just implement INode and bind the root of your tree to the Root property of the TreeEditor
- for a fully working example see Plainion.RI/Controls/TreeEditorView

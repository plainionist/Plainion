using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Plainion.Windows.Controls.Tree;
using Prism.Commands;
using Prism.Mvvm;

namespace Plainion.RI.Controls
{
    [Export]
    class TreeEditorViewModel : BindableBase
    {
        private DragDropBehavior myDragDropBehavior;

        public TreeEditorViewModel()
        {
            Root = new Node();
            Root.IsDragAllowed = false;
            Root.IsDropAllowed = false;

            BuildTree();

            CreateChildCommand = new DelegateCommand<Node>(OnCreateChild, p => p.Parent == Root || p == Root);
            DeleteCommand = new DelegateCommand<Node>(n => ((Node)n.Parent).Children.Remove(n));

            myDragDropBehavior = new DragDropBehavior(Root);
            DropCommand = new DelegateCommand<NodeDropRequest>(myDragDropBehavior.ApplyDrop);

            RefreshCommand = new DelegateCommand(BuildTree);
        }

        private void OnCreateChild(Node parent)
        {
            var node = new Node
            {
                Parent = parent,
                Id = Guid.NewGuid().ToString(),
                Name = "<new>",
                IsDragAllowed = parent != Root,
                IsDropAllowed = parent == Root
            };
            parent.Children.Add(node);

            node.IsSelected = true;
            parent.IsExpanded = true;
        }

        public Node Root { get; private set; }

        public ICommand CreateChildCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand DropCommand { get; private set; }

        public ICommand RefreshCommand { get; private set; }

        private void BuildTree()
        {
            Root.Children.Clear();

            foreach (var process in Process.GetProcesses())
            {
                var processNode = new Node
                {
                    Parent = Root,
                    Id = process.Id.ToString(),
                    Name = process.ProcessName,
                    IsDragAllowed = false
                };
                Root.Children.Add(processNode);

                processNode.Children.AddRange(process.Threads
                    .OfType<ProcessThread>()
                    .Select(t => new Node
                    {
                        Parent = processNode,
                        Id = t.Id.ToString(),
                        Name = "unknown",
                        IsDropAllowed = false
                    }));
            }
        }
    }
}

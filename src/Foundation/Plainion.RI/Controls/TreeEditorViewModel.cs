using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using Prism.Mvvm;

namespace Plainion.RI.Controls
{
    [Export]
    class TreeEditorViewModel : BindableBase
    {
        public TreeEditorViewModel()
        {
            Root = new Node();

            Root.Children = new List<Node>();

            foreach( var process in Process.GetProcesses() )
            {
                var processNode = new Node() { Parent = Root, Id = process.Id.ToString(), Name = process.ProcessName };
                Root.Children.Add( processNode );

                processNode.Children = process.Threads
                    .OfType<ProcessThread>()
                    .Select( t => new Node { Parent = processNode, Id = t.Id.ToString(), Name = "unknown" } )
                    .ToList();
            }
        }

        public Node Root { get; private set; }
    }
}

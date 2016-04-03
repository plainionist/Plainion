using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using ICSharpCode.AvalonEdit.Document;
using Prism.Mvvm;
using Plainion.RI.Editors.Spec;
using Plainion.Windows.Editors.Xml;

namespace Plainion.RI.Editors
{
    [Export]
    class XmlEditorViewModel : BindableBase
    {
        private TextDocument myDocument;
        private IEnumerable<ElementCompletionData> myCompletionData;

        public XmlEditorViewModel()
        {
            Document = new TextDocument();

            myCompletionData = GetType().Assembly.GetTypes()
                .Where( t => t.Namespace == typeof( SystemPackaging ).Namespace )
                .Where( t => !t.IsAbstract )
                .Where( t => t.GetCustomAttributes( typeof( CompilerGeneratedAttribute ), true ).Length == 0 )
                .Select( t => new ElementCompletionData( t ) )
                .ToList();
        }

        public TextDocument Document
        {
            get { return myDocument; }
            set { SetProperty( ref myDocument, value ); }
        }

        public IEnumerable<ElementCompletionData> CompletionData
        {
            get { return myCompletionData; }
            set { SetProperty( ref myCompletionData, value ); }
        }
    }
}

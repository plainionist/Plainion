using System.Printing;
using System.Windows;
using System.Windows.Documents;

namespace Plainion.Prism.Interactivity.InteractionRequest
{
    public interface IPrintRequestAware
    {
        DocumentPaginator GetPaginator( Size printSize );
        PageOrientation PreferredOrientation { get; }
    }
}

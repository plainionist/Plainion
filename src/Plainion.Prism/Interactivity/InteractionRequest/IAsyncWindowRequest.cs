using System.Threading.Tasks;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Plainion.Controls.Interactivity.InteractionRequest
{
    public interface IAsyncWindowRequest
    {
        Task Raise( INotification notification );
    }
}

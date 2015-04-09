using System.Threading.Tasks;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Plainion.Prism.Interactivity.InteractionRequest
{
    public interface IAsyncWindowRequest
    {
        Task Raise( INotification notification );
    }
}

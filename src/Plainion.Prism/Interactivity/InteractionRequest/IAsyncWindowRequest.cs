using System.Threading.Tasks;
using Prism.Interactivity.InteractionRequest;

namespace Plainion.Prism.Interactivity.InteractionRequest
{
    public interface IAsyncWindowRequest
    {
        Task Raise( INotification notification );
    }
}

using System;

namespace Plainion.Prism.Interactivity.InteractionRequest
{
    public interface IAsyncWindowRequestFactory
    {
        IAsyncWindowRequest CreateForView( Type windowContent );
    }
}

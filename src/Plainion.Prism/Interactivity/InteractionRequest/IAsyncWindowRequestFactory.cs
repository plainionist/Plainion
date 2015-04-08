using System;

namespace Plainion.Controls.Interactivity.InteractionRequest
{
    public interface IAsyncWindowRequestFactory
    {
        IAsyncWindowRequest CreateForView( Type windowContent );
    }
}

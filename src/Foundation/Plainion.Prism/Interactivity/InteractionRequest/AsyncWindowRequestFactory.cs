using System;
using System.ComponentModel.Composition;

namespace Plainion.Prism.Interactivity.InteractionRequest
{
    [Export( typeof( IAsyncWindowRequestFactory ) )]
    public class AsyncWindowRequestFactory : IAsyncWindowRequestFactory
    {
        public IAsyncWindowRequest CreateForView( Type windowContent )
        {
            return new AsyncWindowRequest( windowContent );
        }
    }
}

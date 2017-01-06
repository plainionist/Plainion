using System;

namespace Plainion.Composition
{
    /// <summary>
    /// Interface to slightly abstract away MEF CompositionContainer.
    /// </summary>
    public interface IComposer
    {
        void Register( params Type[] types );
        void RegisterInstance<T>( T instance );
        void RegisterInstance<T>( string contractName, T instance );

        T Resolve<T>();
        T Resolve<T>(string contractName);
    }
}

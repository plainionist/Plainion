using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace Plainion.Composition
{
    /// <summary>
    /// Simplify usage of MEF or plain vanilla usages of it.
    /// </summary>
    public class Composer : IComposer, IDisposable
    {
        private AggregateCatalog myCatalog;
        private CompositionBatch myBatch;
        private CompositionContainer myContainer;

        public Composer()
        {
            myCatalog = new AggregateCatalog();
            myBatch = new CompositionBatch();
            myContainer = new CompositionContainer( myCatalog, CompositionOptions.DisableSilentRejection );
        }

        public void Compose()
        {
            myContainer.Compose( myBatch );
        }

        public void Add( ComposablePartCatalog catalog )
        {
            myCatalog.Catalogs.Add( catalog );
        }

        public void Register( params Type[] types )
        {
            myCatalog.Catalogs.Add( new TypeCatalog( types ) );
        }

        public T Resolve<T>()
        {
            return myContainer.GetExportedValue<T>();
        }

        public T Resolve<T>( string contractName )
        {
            return myContainer.GetExportedValue<T>( contractName );
        }

        public void RegisterInstance<T>( T instance )
        {
            myBatch.AddExportedValue<T>( instance );
        }

        public void RegisterInstance<T>( string contractName, T instance )
        {
            myBatch.AddExportedValue<T>( contractName, instance );
        }

        public void Dispose()
        {
            if ( myContainer != null )
            {
                myContainer.Dispose();
                myContainer = null;
            }
        }
    }
}

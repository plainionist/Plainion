using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Linq;
using System.Security;

namespace Plainion.Composition
{
    /// <summary>
    /// Provides MEF support for the decorator pattern. 
    /// This catalog builds a chain of decorators for the contract specified at the constructor and from the types added 
    /// using Add(). Each decorator type has to import and export the "contract to decorate". The last addd type will be 
    /// the most outer decorator which will then satisfy imports requiring the "contract to decorate".
    /// </summary>
    /// <example>
    /// <code>
    /// var decoration = new DecoratorChainCatalog( typeof( IContract ) );
    /// decoration.Add( typeof( Impl ) );
    /// decoration.Add( typeof( Decorator1 ) );
    /// decoration.Add( typeof( Decorator2 ) );
    /// 
    /// // - pass this catalog to MEF CompositionContainer
    /// // - everyone importing IContract will get the Decorator2 which decorates Decorator1 which decorates the actual implementation "Impl".
    /// </code>
    /// </example>
    /// <remarks>
    /// Internally this catalog rewrites the contracts of the parts in the chain to avoid conflicts.
    /// Recomposition is currently not supported.
    /// </remarks>
    public class DecoratorChainCatalog : ComposablePartCatalog
    {
        private List<Type> myDecoratorChain;
        private List<ComposablePartDefinition> myParts;

        private string myContractName;

        /// <summary>
        /// Creates a catalog for the contract specified by the given type.
        /// </summary>
        public DecoratorChainCatalog( Type contract )
            : this( AttributedModelServices.GetContractName( contract ) )
        {
        }

        /// <summary>
        /// Creates a catalog for the contract specified by the given contract name.
        /// </summary>
        public DecoratorChainCatalog( string contract )
        {
            Contract.RequiresNotNullNotEmpty( contract, "contract" );

            myContractName = contract;

            myDecoratorChain = new List<Type>();
            myParts = new List<ComposablePartDefinition>();
        }

        public void Add( Type type )
        {
            Contract.Invariant( !myParts.Any(), "Recomposition not supported" );

            myDecoratorChain.Add( type );
        }

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get
            {
                ComposeDecoration();
                return myParts.AsQueryable();
            }
        }

        [SecuritySafeCritical]
        private void ComposeDecoration()
        {
            if ( myParts.Any() )
            {
                return;
            }

            var contracts = new List<string>();
            foreach ( var type in myDecoratorChain )
            {
                var originalPart = AttributedModelServices.CreatePartDefinition( type, null );

                var importDefs = originalPart.ImportDefinitions.ToList();

                if ( type != myDecoratorChain.First() )
                {
                    RewriteContract( type, importDefs, contracts.Last() );
                }

                var exportDefs = originalPart.ExportDefinitions.ToList();

                if ( type != myDecoratorChain.Last() )
                {
                    contracts.Add( Guid.NewGuid().ToString() );
                    RewriteContract( type, exportDefs, type, contracts.Last() );
                }

                // as we pass it to lazy below we have to copy it to local variable - otherwise we create a closure with the loop iterator variable
                // and this will cause the actual part type to be changed
                var partType = type;
                var part = ReflectionModelServices.CreatePartDefinition(
                    new Lazy<Type>( () => partType ),
                    ReflectionModelServices.IsDisposalRequired( originalPart ),
                    new Lazy<IEnumerable<ImportDefinition>>( () => importDefs ),
                    new Lazy<IEnumerable<ExportDefinition>>( () => exportDefs ),
                    new Lazy<IDictionary<string, object>>( () => new Dictionary<string, object>() ),
                    null );

                myParts.Add( part );
            }

            // no add possible any longer
            myDecoratorChain = null;
        }

        [SecuritySafeCritical]
        private void RewriteContract( Type typeToDecorate, IList<ImportDefinition> importDefs, string newContract )
        {
            var importToDecorate = importDefs.SingleOrDefault( d => d.ContractName == myContractName );
            Contract.Requires( importToDecorate != null, "No import found for contract {0} on type {1}", myContractName, typeToDecorate );

            importDefs.Remove( importToDecorate );

            Contract.Invariant( importToDecorate.Cardinality == ImportCardinality.ExactlyOne, "Decoration of Cardinality " + importToDecorate.Cardinality + " not supported" );
            Contract.Invariant( ReflectionModelServices.IsImportingParameter( importToDecorate ), "Decoration of property injection not supported" );

            var param = ReflectionModelServices.GetImportingParameter( importToDecorate );
            var importDef = ReflectionModelServices.CreateImportDefinition(
                param,
                newContract,
                AttributedModelServices.GetTypeIdentity( param.Value.ParameterType ),
                Enumerable.Empty<KeyValuePair<string, Type>>(),
                importToDecorate.Cardinality,
                CreationPolicy.Any,
                null );

            importDefs.Add( importDef );
        }

        [SecuritySafeCritical]
        private void RewriteContract( Type typeToDecorate, IList<ExportDefinition> exportDefs, Type exportingType, string newContract )
        {
            var exportToDecorate = exportDefs.Single( d => d.ContractName == myContractName );
            Contract.Requires( exportToDecorate != null, "No import found for contract {0} on type {1}", myContractName, typeToDecorate );

            exportDefs.Remove( exportToDecorate );

            var exportDef = ReflectionModelServices.CreateExportDefinition(
                new LazyMemberInfo( exportingType ),
                newContract,
                new Lazy<IDictionary<string, object>>( () => exportToDecorate.Metadata ),
                null );

            exportDefs.Add( exportDef );
        }
    }
}

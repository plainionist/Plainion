using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.ComponentModel.Composition;
using System.Security;
using System.Diagnostics;
using System.Reflection;

namespace Blade.Composition
{
    public class DecoratorChainCatalog : ComposablePartCatalog
    {
        private List<Type> myDecoratorChain;
        private List<ComposablePartDefinition> myParts;

        private string myContractName;

        public DecoratorChainCatalog( Type contract )
            : this( AttributedModelServices.GetContractName( contract ) )
        {
        }

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


# Plainion.Composition.DecoratorChainCatalog

**Namespace:** Plainion.Composition

**Assembly:** Plainion.Core

Provides MEF support for the decorator pattern. This catalog builds a chain of decorators for the contract specified at the constructor and from the types added using Add(). Each decorator type has to import and export the "contract to decorate". The last addd type will be the most outer decorator which will then satisfy imports requiring the "contract to decorate".

## Remarks

Internally this catalog rewrites the contracts of the parts in the chain to avoid conflicts. Recomposition is currently not supported.

## Example


```
var decoration = new DecoratorChainCatalog( typeof( IContract ) );
            decoration.Add( typeof( Impl ) );
            decoration.Add( typeof( Decorator1 ) );
            decoration.Add( typeof( Decorator2 ) );
            
            // - pass this catalog to MEF CompositionContainer
            // - everyone importing IContract will get the Decorator2 which decorates Decorator1 which decorates the actual implementation "Impl".
```


## Constructors

### Constructor(System.Type contract)

Creates a catalog for the contract specified by the given type.

### Constructor(System.String contract)

Creates a catalog for the contract specified by the given contract name.


## Properties

### System.Linq.IQueryable`1[[System.ComponentModel.Composition.Primitives.ComposablePartDefinition, System.ComponentModel.Composition, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] Parts


## Methods

### void Add(System.Type type)

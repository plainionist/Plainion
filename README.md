
The Plainion.Core library provides various extensions to the .NET framework to simplify the usage of the
underlying .NET concepts. 

![NuGet Version](https://img.shields.io/nuget/v/Plainion.Core.svg?style=flat-square)

## Plainion

- [Contract](doc/Api/Plainion.Core/Plainion/Contract.md):
  Extremely simple but still powerful implementation of Design-By-Contract

## Plainion.Composition

Provides extensions to MEF

- [IComposer](doc/Api/Plainion.Core/Plainion/Composition/IComposer.md): slim wrapper around MEF CompositionContainer, Catalogs and CompositionBatch
- [DecoratorChainCatalog](doc/Api/Plainion.Core/Plainion/Composition/DecoratorChainCatalog.md): adds decorator pattern support to MEF

## Plainion.IO

Provides filesystem abstractions and an in-memory filesystem implementation which makes unittesting
of code with filesystem dependencies much easier.

See [ReadMe](src/Plainion.Core/IO/ReadMe.md)

## Plainion.Logging

Provides simple logging framework abstraction to keep most of an application code base "logging framework implementation independent".

See [ReadMe](src/Plainion.Core/Logging/ReadMe.md)

## Misc

Many more tiny little helpers

- [SerializableBindableBase](doc/Api/Plainion.Core/Plainion/Serialization/SerializableBindableBase.md) : Serializable version of the BindableBase from Prism 
- [StartSTATask](doc/Api/Plainion.Core/Plainion/Tasks/Tasks.md) : Start a TPL task as STA thread
- [RecursiveValidator](doc/Api/Plainion.Core/Plainion/Validation/RecursiveValidator.md) : Verifies an object graph recursively according to applied DataAnnotations


using System.ComponentModel.Composition;
using NUnit.Framework;
using Plainion.Composition;

namespace Plainion.Tests.Composition
{
    [TestFixture]
    public class DecoratorChainCatalogTests
    {
        [Test]
        public void Compose_DecoratorGiven_DecoratedInstanceIsImported()
        {
            var composer = new Composer();
            composer.Register( typeof( User ) );

            var decoration = new DecoratorChainCatalog( typeof( IContract ) );
            decoration.Add( typeof( Impl ) );
            decoration.Add( typeof( Decorator1 ) );
            decoration.Add( typeof( Decorator2 ) );

            composer.Add( decoration );

            composer.Compose();

            var user = composer.Resolve<User>();

            Assert.That( user.Contract, Is.InstanceOf<Decorator2>() );
        }

        private interface IContract
        {
        }

        [Export( typeof( Impl ) )]
        [Export( typeof( IContract ) )]
        private class Impl : IContract
        {
        }

        [Export( typeof( Decorator1 ) )]
        [Export( typeof( IContract ) )]
        private class Decorator1 : IContract
        {
            [ImportingConstructor]
            public Decorator1( IContract realContract ) { }
        }

        [Export( typeof( Decorator2 ) )]
        [Export( typeof( IContract ) )]
        private class Decorator2 : IContract
        {
            [ImportingConstructor]
            public Decorator2( IContract realContract ) { }
        }

        [Export]
        private class User
        {
            [Import]
            public IContract Contract { get; set; }
        }
    }
}

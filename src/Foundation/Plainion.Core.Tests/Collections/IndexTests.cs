using NUnit.Framework;
using Plainion.Collections;

namespace Plainion.Tests.Collections
{
    [TestFixture]
    class IndexTests
    {
        [Test]
        public void Indexer_KeyExists_ExistingValueIsReturned()
        {
            var index = new Index<int, string>( i => i.ToString() );

            index.Add( 5 );

            Assert.That( index[ 5 ], Is.EqualTo( "5" ) );
        }

        [Test]
        public void Indexer_KeyDoesNotExists_NewValueIsCreated()
        {
            var index = new Index<int, string>( i => i.ToString() );

            Assert.That( index[ 5 ], Is.EqualTo( "5" ) );
        }

        [Test]
        public void TryGetValue_KeyDoesNotExists_NoEntryIsCreated()
        {
            var index = new Index<int, string>( i => i.ToString() );

            string value;
            index.TryGetValue( 5, out value );

            Assert.That( index.Keys, Is.Empty );
            Assert.That( index.Values, Is.Empty );
        }

        [Test]
        public void TryGetValue_KeyDoesNotExists_ReturnsFalse()
        {
            var index = new Index<int, string>( i => i.ToString() );

            string value;
            var ret = index.TryGetValue( 5, out value );

            Assert.That( ret, Is.False );
        }

        [Test]
        public void TryGetValue_KeyExists_ReturnsTrue()
        {
            var index = new Index<int, string>( i => i.ToString() );

            index.Add( 5 );
            
            string value;
            var ret = index.TryGetValue( 5, out value );

            Assert.That( ret, Is.True );
        }
    }
}

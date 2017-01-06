using System;
using System.Linq;
using NUnit.Framework;
using Plainion.Collections;

namespace Plainion.Tests.Collections
{
    [TestFixture]
    class EnumerableTests
    {
        [Test]
        public void IndexOf_EmptyCollection_ReturnsMinusOne()
        {
            Assert.That( Enumerable.Empty<string>().IndexOf( "some" ), Is.EqualTo( -1 ) );
        }

        [Test]
        public void IndexOf_WhenCalled_CorrectIndexIsReturned()
        {
            Assert.That( new[] { 1, 2, 3, 4, 5 }.IndexOf( 3 ), Is.EqualTo( 2 ) );
        }

        [Test]
        public void IndexOf_MultipleOccurences_FirstPositionReturned()
        {
            Assert.That( new[] { 1, 2, 3, 3, 3 }.IndexOf( 3 ), Is.EqualTo( 2 ) );
        }

        [Test]
        public void IndexOf_EmptyCollectionAndPredicate_ReturnsMinusOne()
        {
            Assert.That( Enumerable.Empty<string>().IndexOf( x => x == "some" ), Is.EqualTo( -1 ) );
        }

        [Test]
        public void IndexOf_WhenCalledWithPredicate_CorrectIndexIsReturned()
        {
            Assert.That( new[] { 1, 2, 3, 4, 5 }.IndexOf( x => x == 3 ), Is.EqualTo( 2 ) );
        }

        [Test]
        public void IndexOf_MultipleOccurencesAndPredicate_FirstPositionReturned()
        {
            Assert.That( new[] { 1, 2, 3, 3, 3 }.IndexOf( x => x == 3 ), Is.EqualTo( 2 ) );
        }
    }
}

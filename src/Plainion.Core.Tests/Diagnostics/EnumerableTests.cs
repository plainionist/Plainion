using System;
using NUnit.Framework;
using Plainion.Diagnostics;

namespace Plainion.Tests
{
    [TestFixture]
    class EnumerableTests
    {
        [Test]
        public void ToHuman_Null_EmptyStringReturned()
        {
            Assert.That( Enumerable.ToHuman( null ), Is.EqualTo( string.Empty ) );
        }

        [Test]
        public void ToHuman_CollectionWithValues_ReturnsHumanRepresentation()
        {
            Assert.That( Enumerable.ToHuman( new int[] { 5, 7 } ), Is.EqualTo( "[5|7]" ) );
        }
    }
}

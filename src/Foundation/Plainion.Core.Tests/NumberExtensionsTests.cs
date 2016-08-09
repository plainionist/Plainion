using System;
using NUnit.Framework;

namespace Plainion.Tests
{
    [TestFixture]
    class NumberExtensionsTests
    {
        [Test]
        public void Times_WithZero_NoActionExecuted()
        {
            var count = 0;

            0.Times( i => count++ );

            Assert.That( count, Is.EqualTo( 0 ) );
        }

        [Test]
        public void Times_WhenCalled_ActionCalledAsOftenAsSpecified()
        {
            var count = 0;

            5.Times( i => count++ );

            Assert.That( count, Is.EqualTo( 5 ) );
        }

        [Test]
        public void Times_NegativeNumber_Throws()
        {
            var count = 0;

            Assert.Throws<ArgumentException>( () => ( -7 ).Times( i => count++ ) );
        }
    }
}

using System;
using NUnit.Framework;

namespace Plainion.Tests
{
    [TestFixture]
    class StringExtensionsTests
    {
        [Test]
        public void IsTrue_WithTrueStrings_ReturnsTrue( [Values( "yes", "on", "true", "y" )] string value )
        {
            Assert.That( value.IsTrue(), Is.True );
        }

        [Test]
        public void IsTrue_WithFalseStrings_ReturnsFalse( [Values( "no", "off", "false", "n" )] string value )
        {
            Assert.That( value.IsTrue(), Is.False );
        }

        [Test]
        public void IsTrue_WithArbitraryString_ReturnsFalse()
        {
            Assert.That( "some dummy string".IsTrue(), Is.False );
        }

        [Test]
        public void RemoveAll_WithMultipleFindings_AllRemoved()
        {
            var actual = "oh ... too much o's".RemoveAll( c => c == 'o' );

            Assert.That( actual, Is.EqualTo( "h ... t much 's" ) );
        }

    }
}

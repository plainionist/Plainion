using NUnit.Framework;
using Plainion.Text;

namespace Plainion.Core.Tests.Text
{
    [TestFixture]
    class WildcardTests
    {
        [Test]
        public void IsMatch_SubstringWithoutLeadingWildcard_Fails()
        {
            Assert.That( Wildcard.IsMatch( "dummy", "mm*" ), Is.False );
        }

        [Test]
        public void IsMatch_SubstringWithoutTrailingWildcard_Fails()
        {
            Assert.That( Wildcard.IsMatch( "dummy", "*mm" ), Is.False );
        }

        [Test]
        public void IsMatch_SubstringWithLeadingWildcard_Succeeds()
        {
            Assert.That( Wildcard.IsMatch( "dummy", "*mmy" ), Is.True );
        }

        [Test]
        public void IsMatch_SubstringWithTrailingWildcard_Succeeds()
        {
            Assert.That( Wildcard.IsMatch( "dummy", "dum*" ), Is.True );
        }

        [Test]
        public void IsMatch_WildcardInMiddle_Succeeds()
        {
            Assert.That( Wildcard.IsMatch( "dummy", "d*y" ), Is.True );
        }

        [Test]
        public void IsMatch_SingleWildcardReplacesOneCharacter_Succeeds()
        {
            Assert.That( Wildcard.IsMatch( "dummy", "d?mmy" ), Is.True );
        }

    }
}

using System;
using NUnit.Framework;

namespace Plainion.Tests
{
    [TestFixture]
    class ObjectsTests
    {
        [Test]
        public void Clone_SimpleType_ClonesContent()
        {
            var x = new DTO { Value = 27 };

            var clone = Objects.Clone( x );

            Assert.That( clone.Value, Is.EqualTo( x.Value ) );
        }

        [Serializable]
        private class DTO
        {
            public int Value { get; set; }
        }
    }
}

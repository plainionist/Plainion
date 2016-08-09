using System;
using System.Collections.Generic;
using NUnit.Framework;
using Plainion.Collections;

namespace Plainion.Tests.Collections
{
    [TestFixture]
    class IListExtensionsTests
    {
        [Test]
        public void AddRange_RangeIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>( () => IListTExtensions.AddRange( new List<string>(), null ) );
        }

        [Test]
        public void AddRange_EmptyCollection_NothingAdded()
        {
            var list = new List<string>();

            IListTExtensions.AddRange( list, new List<string>() );

            Assert.That( list, Is.Empty );
        }

        [Test]
        public void AddRange_CollectionPassed_AllValuesAdded()
        {
            var list = new List<int>();

            IListTExtensions.AddRange( list, new[] { 5, 2, 8 } );

            Assert.That( list, Is.EquivalentTo( new[] { 5, 2, 8 } ) );
        }
    }
}

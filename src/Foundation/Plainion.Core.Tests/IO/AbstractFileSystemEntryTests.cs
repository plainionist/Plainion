using System;
using NUnit.Framework;
using Plainion.IO;
using FS = Plainion.IO.MemoryFS.FileSystemImpl;

namespace Plainion.Core.Tests.IO
{
    [TestFixture]
    class AbstractFileSysteEmnrtryTests
    {
        [Test]
        public void Parent_WithFileSystemRoot_ReturnsNull()
        {
            var entry = new FS().Directory( @"c:" );

            Assert.That( entry.Parent, Is.Null );
        }

        [Test]
        public void Parent_SomePath_ParentReturned()
        {
            var entry = new FS().Directory( @"c:\temp\xyz" );

            Assert.That( entry.Parent.Path, Is.EqualTo( @"c:\temp" ) );
        }

        [Test]
        public void Equals_DifferentPath_ReturnsFalse()
        {
            var lhs = new FS().File( @"c:\temp\xyz" );
            var rhs = new FS().File( @"c:\temp\ABC" );

            Assert.That( lhs.Equals( rhs ), Is.False );
        }

        [Test]
        public void Equals_SamePath_ReturnsTrue()
        {
            var lhs = new FS().File( @"c:\temp\xyz" );
            var rhs = new FS().File( @"c:\temp\xyz\" );

            Assert.That( lhs.Equals( rhs ), Is.True );
        }
    }
}

using System;
using NUnit.Framework;
using Plainion.IO;
using FS = Plainion.IO.MemoryFS.FileSystemImpl;

namespace Plainion.Tests.IO
{
    [TestFixture]
    class FileSystemOperationsTests
    {
        /// <summary>
        /// Implicitly tests "ReadAllLines"
        /// </summary>
        [Test]
        public void WriteAll_MultipleLines_AllLinesWritten()
        {
            var file = new FS().File( @"/some/folder/file" );

            file.WriteAll( "line1", "line2", "line3" );

            Assert.That( file.ReadAllLines(), Is.EquivalentTo( new[] { "line1", "line2", "line3" } ) );
        }
    }
}

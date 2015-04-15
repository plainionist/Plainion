using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using Plainion.IO.MemoryFS;

namespace Plainion.UnitTests.IO.MemoryFS
{
    [TestFixture]
    public class FileSystemImplTest
    {
        [Test]
        public void GetTempPath_WhenCalled_AlwaysExists()
        {
            var fs = new FileSystemImpl();

            var dir = fs.GetTempPath();

            Assert.That( dir.Exists, Is.True );
        }

        [Test]
        public void GetTempFile_WhenCalled_ParentAlwaysExists()
        {
            var fs = new FileSystemImpl();

            var file = fs.GetTempFile();

            Assert.That( file.Parent.Exists, Is.True );
        }
    }
}

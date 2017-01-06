using NUnit.Framework;
using Plainion.IO.MemoryFS;

namespace Plainion.Tests.IO.MemoryFS
{
    [TestFixture]
    public class FileSystemImplTests
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

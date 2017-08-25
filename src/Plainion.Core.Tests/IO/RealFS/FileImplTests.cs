using System;
using System.IO;
using NUnit.Framework;
using Plainion.IO.RealFS;

namespace Plainion.Tests.IO.RealFS
{
    [TestFixture]
    class FileImplTests
    {
        [Test]
        public void Stream_NewFiles_CreatedOnDemand()
        {
            var fs = new FileSystemImpl();
            var file = fs.Directory(Path.GetTempPath()).File(Guid.NewGuid().ToString());

            using (var stream = file.Stream(FileAccess.Write))
            {
                stream.WriteByte(0);
            }

            Assert.That(file.Exists);

            file.Delete();
        }
    }
}

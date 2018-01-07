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

        [Test]
        public void Stream_OpenWithRead_ContentCanBeRead()
        {
            var fs = new FileSystemImpl();
            var file = fs.Directory(Path.GetTempPath()).File(Guid.NewGuid().ToString());

            using (var stream = file.Stream(FileAccess.Write))
            {
                stream.WriteByte(2);
                stream.WriteByte(3);
                stream.WriteByte(4);
            }

            using (var stream = file.Stream(FileAccess.Read))
            {
                Assert.That(stream.ReadByte(), Is.EqualTo(2));
                Assert.That(stream.ReadByte(), Is.EqualTo(3));
                Assert.That(stream.ReadByte(), Is.EqualTo(4));
            }

            file.Delete();
        }

        [Test]
        public void Stream_ExistingFiles_ContentFullyOverwritten()
        {
            var fs = new FileSystemImpl();
            var file = fs.Directory(Path.GetTempPath()).File(Guid.NewGuid().ToString());

            using (var stream = file.Stream(FileAccess.Write))
            {
                stream.WriteByte(9);
                stream.WriteByte(9);
                stream.WriteByte(9);
                stream.WriteByte(9);
                stream.WriteByte(9);
            }

            using (var stream = file.Stream(FileAccess.Write))
            {
                stream.WriteByte(1);
                stream.WriteByte(1);
                stream.WriteByte(1);
            }

            Assert.That(file.Size, Is.EqualTo(3));

            file.Delete();
        }
    }
}

using Plainion.IO;
using NUnit.Framework;

namespace Plainion.Tests.IO
{
    [TestFixture]
    class FileSystemTests
    {
        [Test]
        public void UnifyPath_NoTrailingPathSeparator_PathUnchanged()
        {
            Assert.That( FileSystem.UnifyPath( @"c:\test" ), Is.EqualTo( @"c:\test" ) );
        }

        [Test]
        public void UnifyPath_WithTrailingPathSeparator_SeparatorRemoved( [Values( "/", "\\" )] string sep )
        {
            Assert.That( FileSystem.UnifyPath( @"c:\test" + sep ), Is.EqualTo( @"c:\test" ) );
        }

        [Test]
        public void UnifyPath_DriveLetterOnly_SameStringReturned()
        {
            Assert.That( FileSystem.UnifyPath( "c:" ), Is.EqualTo( "c:" ) );
        }

        [Test]
        public void UnifyPath_DriveLetterWithBackslash_BackslashRemoved()
        {
            Assert.That( FileSystem.UnifyPath( @"c:\" ), Is.EqualTo( "c:" ) );
        }
    }
}

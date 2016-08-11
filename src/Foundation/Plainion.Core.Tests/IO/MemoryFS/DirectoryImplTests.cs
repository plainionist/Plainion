using System.IO;
using NUnit.Framework;
using Plainion.IO;
using Plainion.IO.MemoryFS;

namespace Plainion.Tests.IO.MemoryFS
{
    [TestFixture]
    public class DirectoryImplTests
    {
        private FileSystemImpl myFileSystem;

        // as we always convert filenames into absolute paths this must be 
        // an valid absolute path beforehand to simplify assertions
        private static string SimplestDirectoryPossible = @"c:\a\";

        [SetUp]
        public void SetUp()
        {
            myFileSystem = new FileSystemImpl();
        }

        [Test]
        public void Ctor_WhenCalled_ExistsReturnsFalse()
        {
            var dir = myFileSystem.Directory( SimplestDirectoryPossible );

            Assert.That( dir.Exists, Is.False );
        }

        [Test]
        public void Ctor_WithDriveRoot_ExistsReturnsTrue()
        {
            var dir = myFileSystem.Directory( "c:" );

            Assert.That( dir.Exists, Is.True );
        }

        [Test]
        public void Create_WhenCalled_ExistsReturnsTrue()
        {
            var dir = myFileSystem.Directory( SimplestDirectoryPossible );

            dir.Create();

            Assert.That( dir.Exists, Is.True );
        }

        [Test]
        public void Create_WhenCalled_ParentDirectoriesWillBeCreatedOnDemand()
        {
            var dir = myFileSystem.Directory( SimplestDirectoryPossible );
            var subDir = dir.Directory( "subDir" );

            subDir.Create();

            Assert.That( subDir.Exists, Is.True );
        }

        [Test]
        public void GetFiles_WithEmptyDirectory_ReturnsEmptyList()
        {
            var dir = myFileSystem.Directory( SimplestDirectoryPossible );
            dir.Create();

            var files = dir.EnumerateFiles();

            Assert.That( files, Is.Empty );
        }

        [Test]
        public void GetFiles_DirectoryContainsFilesAndDirectories_ReturnsCreatedFilesOnly()
        {
            var dir = myFileSystem.Directory( SimplestDirectoryPossible );
            dir.Create();

            var f1 = dir.File( "f1" );
            dir.Directory( "d" );
            var f2 = dir.File( "f2" );
            var f3 = dir.File( "f3" );

            f1.Create();
            f2.Create();
            // for f3 only the wrapper created has been created but not the file

            var files = dir.EnumerateFiles();

            Assert.That( files, Is.EquivalentTo( new IFile[] { f1, f2 } ) );
        }

        [Test]
        public void GetFiles_WithPatternAndNonEmptyDirectory_ReturnsMatchingFilesOnly()
        {
            var dir = myFileSystem.Directory( SimplestDirectoryPossible );
            dir.Create();

            var f1 = dir.File( "f1.txt" );
            var f2 = dir.File( "f2.txt" );
            var f3 = dir.File( "ff1.log" );

            f1.Create();
            f2.Create();
            f3.Create();

            var files = dir.EnumerateFiles( "*.txt" );

            Assert.That( files, Is.EquivalentTo( new IFile[] { f1, f2 } ) );
        }

        [Test]
        public void GetFiles_Recursively_ReturnsAllFiles()
        {
            var dir = myFileSystem.Directory( SimplestDirectoryPossible );
            dir.Create();

            var f1 = dir.File( "f1" );
            var f2 = dir.Directory( "d" ).File( "f2" );
            f1.Create();
            f2.Parent.Create();
            f2.Create();

            var files = dir.EnumerateFiles( "*", SearchOption.AllDirectories );

            Assert.That( files, Is.EquivalentTo( new IFile[] { f1, f2 } ) );
        }

        [Test]
        public void Delete_WithEmptyDirectory_ExistsShouldReturnFalse()
        {
            var dir = myFileSystem.Directory( SimplestDirectoryPossible );
            dir.Create();

            dir.Delete();

            Assert.That( dir.Exists, Is.False );
        }

        [Test]
        public void Delete_WithNonEmptyDirectory_ShouldThrow()
        {
            var dir = myFileSystem.Directory( SimplestDirectoryPossible );
            dir.Create();
            dir.File( "f1" ).Create();

            Assert.Throws<IOException>( () => dir.Delete() );
        }

        [Test]
        public void Delete_DirectoryWithContentAndRecursiveTrue_ShouldDeleteDirectoryWithContent()
        {
            var dir = myFileSystem.Directory( SimplestDirectoryPossible );
            var subDir = dir.Directory( "subDir" );
            var f1 = dir.File( "f1" );
            var f2 = subDir.File( "f2" );
            subDir.Create();
            f1.Create();
            f2.Create();

            dir.Delete( true );

            Assert.That( dir.Exists, Is.False );
            Assert.That( subDir.Exists, Is.False );
            Assert.That( f1.Exists, Is.False );
            Assert.That( f2.Exists, Is.False );
        }
    }
}

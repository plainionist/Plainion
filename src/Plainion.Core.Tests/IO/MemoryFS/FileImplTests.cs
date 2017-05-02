using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using NUnit.Framework;
using Plainion.IO;
using Plainion.IO.MemoryFS;

namespace Plainion.Tests.IO.MemoryFS
{
    [TestFixture]
    public class FileImplTests
    {
        private FileSystemImpl myFileSystem;

        // as we always convert filenames into absolute paths this must be 
        // an valid absolute path beforehand to simplify assertions
        private static string SimplestFilenamePossible = @"c:\a";

        [SetUp]
        public void SetUp()
        {
            myFileSystem = new FileSystemImpl();
        }

        [Test]
        public void Ctor_WhenCalled_ExistsReturnsFalse()
        {
            var file = myFileSystem.File( SimplestFilenamePossible );

            Assert.That( file.Exists, Is.False );
        }

        [Test]
        public void Create_WhenCalled_ExistsReturnsTrue()
        {
            var file = myFileSystem.File( SimplestFilenamePossible );

            file.Create();

            Assert.That( file.Exists, Is.True );
        }

        [Test]
        public void Create_ParentDirectoryDoesNotExist_ParentGetsCreated()
        {
            var file = myFileSystem.File( @"c:\dir\f1.txt" );

            file.Create();

            Assert.That( file.Parent.Exists, Is.True );
        }

        [Test]
        public void Create_WhenCalled_FileIsEmpty()
        {
            var file = myFileSystem.File( SimplestFilenamePossible );

            file.Create();

            var content = file.ReadAllLines();
            Assert.That( content, Is.Empty );
        }

        [Test]
        public void Delete_WhenCalled_ExistsReturnsFalse()
        {
            var file = CreateSampleFile();

            file.Delete();

            Assert.That( file.Exists, Is.False );
        }

        [Test]
        public void CreateWriter_WhenCalledAndWritten_ContentIsFilled()
        {
            var file = CreateSampleFile();

            using( var writer = file.CreateWriter() )
            {
                writer.WriteLine( "a" );
            }

            var content = file.ReadAllLines();
            Assert.That( content, Is.EquivalentTo( new string[] { "a" } ) );
        }

        [Test]
        public void CreateReader_WhenCalledAndRead_ContentWillBeReturned()
        {
            var file = CreateSampleFile();

            var expectedContent = new string[] { "line1", "line2" };
            file.WriteAll( expectedContent );

            var content = new List<string>();
            using( var reader = file.CreateReader() )
            {
                while( reader.Peek() != -1 )
                {
                    content.Add( reader.ReadLine() );
                }
            }

            Assert.That( content, Is.EquivalentTo( expectedContent ) );
        }

        [Test]
        public void CreateReader_WhenCalled_TimestampsShouldBeUpdatedProperly()
        {
            var file = CreateSampleFile();

            var beforeLastWriteTime = file.LastWriteTime;
            var beforeLastAccessTime = file.LastAccessTime;

            // as DateTime is not that precise we have to wait a bit here
            Thread.Sleep( 5 );

            using( var reader = file.CreateReader() )
            {
            }

            Assert.That( beforeLastAccessTime < file.LastAccessTime );
            Assert.That( beforeLastWriteTime == file.LastWriteTime );
        }

        [Test]
        public void CreateWriter_WhenCalled_TimestampsShouldBeUpdatedProperly()
        {
            var file = CreateSampleFile();

            var beforeLastWriteTime = file.LastWriteTime;
            var beforeLastAccessTime = file.LastAccessTime;

            // as DateTime is not that precise we have to wait a bit here
            Thread.Sleep( 5 );

            using( var writer = file.CreateWriter() )
            {
            }

            Assert.That( beforeLastAccessTime == file.LastAccessTime );
            Assert.That( beforeLastWriteTime < file.LastWriteTime );
        }

        [Test]
        public void MoveTo_WhenCalled_FileGetsMoved()
        {
            var file = myFileSystem.File( @"c:\a\b.txt" );
            file.Create();
            file.WriteAll( "hi" );

            var targetFile = file.MoveTo( myFileSystem.Directory( @"c:\d" ) );

            Assert.That( file.Exists, Is.False );
            Assert.That( targetFile.Exists, Is.True );
            Assert.That( targetFile.ReadAllLines(), Is.EquivalentTo( new[] { "hi" } ) );
        }

        [Test]
        public void CopyTo_WhenCalled_FileGetsCopied()
        {
            var file = myFileSystem.File( @"c:\a\b.txt" );
            file.Create();
            file.WriteAll( "hi" );

            var targetFile = file.CopyTo( myFileSystem.Directory( @"c:\d" ), true );

            Assert.That( file.Exists, Is.True );
            Assert.That( targetFile.Exists, Is.True );
            Assert.That( targetFile.ReadAllLines(), Is.EquivalentTo( new[] { "hi" } ) );
        }


        [Test]
        public void Stream_WhenWritten_ContentIsFilled()
        {
            var file = CreateSampleFile();

            using(var writer = new StreamWriter(file.Stream(FileAccess.Write)))
            {
                writer.WriteLine("a");
            }

            var content = file.ReadAllLines();
            Assert.That(content, Is.EquivalentTo(new string[] { "a" }));
        }

        [Test]
        public void Stream_WhenRead_ContentWillBeReturned()
        {
            var file = CreateSampleFile();

            var expectedContent = new string[] { "line1", "line2" };
            file.WriteAll(expectedContent);

            var content = new List<string>();
            using(var reader = new StreamReader(file.Stream(FileAccess.Read)))
            {
                while(reader.Peek() != -1)
                {
                    content.Add(reader.ReadLine());
                }
            }

            Assert.That(content, Is.EquivalentTo(expectedContent));
        }

        private IFile CreateSampleFile()
        {
            var file = myFileSystem.File( SimplestFilenamePossible );
            file.Parent.Create();
            file.Create();

            return file;
        }
    }
}

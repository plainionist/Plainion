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
        public void Create_ParentDirectoryDoesNotExist_Throws()
        {
            var file = myFileSystem.File( @"c:\dir\f1.txt" );

            Assert.Throws<FileNotFoundException>( () => file.Create() );
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

            using ( var writer = file.CreateWriter() )
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
            using ( var reader = file.CreateReader() )
            {
                while ( reader.Peek() != -1 )
                {
                    content.Add( reader.ReadLine() );
                }
            }

            Assert.That( content, Is.EquivalentTo( expectedContent ) );
        }

        [Test]
        public void AllReadActions_WhenCalled_TimestampsShouldBeUpdatedProperly( [Values( "CreateReader", "ReadAllLines" )] string readActionName )
        {
            var file = CreateSampleFile();

            var beforeLastWriteTime = file.LastWriteTime;
            var beforeLastAccessTime = file.LastAccessTime;

            // as DateTime is not that precise we have to wait a bit here
            Thread.Sleep( 5 );

            var readAction = GetAction( readActionName );
            readAction( file );

            Assert.That( beforeLastAccessTime < file.LastAccessTime );
            Assert.That( beforeLastWriteTime == file.LastWriteTime );
        }

        [Test]
        public void AllWriteActions_WhenCalled_TimestampsShouldBeUpdatedProperly( [Values( "CreateWriter", "WriteAll" )] string writeActionName )
        {
            var file = CreateSampleFile();

            var beforeLastWriteTime = file.LastWriteTime;
            var beforeLastAccessTime = file.LastAccessTime;

            // as DateTime is not that precise we have to wait a bit here
            Thread.Sleep( 5 );

            var writeAction = GetAction( writeActionName );
            writeAction( file );

            Assert.That( beforeLastAccessTime == file.LastAccessTime );
            Assert.That( beforeLastWriteTime < file.LastWriteTime );
        }

        private Action<IFile> GetAction( string actionName )
        {
            if ( actionName == "CreateReader" )
            {
                return file => { using ( var reader = file.CreateReader() ) { } };
            }
            else if ( actionName == "ReadAllLines" )
            {
                return file => file.ReadAllLines();
            }
            else if ( actionName == "CreateWriter" )
            {
                return file => { using ( var writer = file.CreateWriter() ) { } };
            }
            else if ( actionName == "WriteAll" )
            {
                return file => file.WriteAll();
            }

            throw new NotSupportedException( actionName );
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

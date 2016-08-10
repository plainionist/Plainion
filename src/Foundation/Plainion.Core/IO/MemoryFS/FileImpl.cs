using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Plainion.IO.MemoryFS
{
    [DataContract( Namespace = DCNames.NS_MemoryFS )]
    internal class FileImpl : AbstractFileSystemEntry<FileSystemImpl>, IFile
    {
        private StringBuilder myContent;

        [DataMember( Name = "Exists" )]
        private bool myExists;
        [DataMember( Name = "LastWriteTime" )]
        private DateTime myLastWriteTime;
        [DataMember( Name = "LastAccessTime" )]
        private DateTime myLastAccessTime;

        [DataMember( Name = "Content" )]
        private string mySerializingContent;

        [OnSerializing]
        private void OnSerializing( StreamingContext context )
        {
            if( myExists && myContent != null )
            {
                mySerializingContent = myContent.ToString();
            }
        }

        [OnSerialized]
        private void OnSerialized( StreamingContext context )
        {
            mySerializingContent = null;
        }

        [OnDeserialized]
        private void OnDeserialized( StreamingContext context )
        {
            if( mySerializingContent != null )
            {
                myContent = new StringBuilder( mySerializingContent );
                mySerializingContent = null;
            }
        }

        public FileImpl( FileSystemImpl fileSystem, string path )
            : base( fileSystem, path )
        {
            myExists = false;
            myContent = null;
            myLastAccessTime = DateTime.MinValue;
            myLastWriteTime = DateTime.MinValue;
        }

        public override bool Exists
        {
            get { return myExists; }
        }

        public override void Create()
        {
            if( Exists )
            {
                return;
            }

            if( !Parent.Exists )
            {
                throw new FileNotFoundException( Parent.Path );
            }

            myExists = true;
            myContent = new StringBuilder();
        }

        public override void Delete()
        {
            myExists = false;
            myContent = null;
        }

        public TextWriter CreateWriter()
        {
            CreateOnDemand();

            myLastWriteTime = DateTime.Now;

            myContent.Clear();
            return new StringWriter( myContent );
        }

        private void CreateOnDemand()
        {
            if( !Exists )
            {
                Create();
            }
        }

        public TextWriter CreateWriter( Encoding encoding )
        {
            return CreateWriter();
        }

        public TextReader CreateReader()
        {
            CheckExists();

            myLastAccessTime = DateTime.Now;

            return new StringReader( myContent.ToString() );
        }

        private void CheckExists()
        {
            if( !Exists )
            {
                throw new FileNotFoundException( Path );
            }
        }

        public TextReader CreateReader( Encoding encoding )
        {
            return CreateReader();
        }

        public override DateTime LastWriteTime
        {
            get
            {
                CheckExists();

                return myLastWriteTime;
            }
        }

        public override DateTime LastAccessTime
        {
            get
            {
                CheckExists();

                return myLastAccessTime;
            }
        }

        public void WriteAll( params string[] text )
        {
            CreateOnDemand();

            myLastWriteTime = DateTime.Now;

            myContent.Clear();
            foreach( var line in text )
            {
                myContent.AppendLine( line );
            }
        }

        public string[] ReadAllLines()
        {
            CheckExists();

            myLastAccessTime = DateTime.Now;

            return GetLines( myContent.ToString() ).ToArray();
        }

        private static IEnumerable<string> GetLines( string str )
        {
            var reader = new StringReader( str );

            var line = reader.ReadLine();
            while( line != null )
            {
                yield return line;
                line = reader.ReadLine();
            }
        }

        public IFile MoveTo( IDirectory directory )
        {
            if( !directory.Exists )
            {
                directory.Create();
            }

            var targetFile = directory.File( Name );
            targetFile.WriteAll( ReadAllLines() );

            Delete();

            return targetFile;
        }

        public IFile CopyTo( IFileSystemEntry dirOrFile, bool overwrite )
        {
            var directory = dirOrFile as IDirectory;
            if( directory == null )
            {
                directory = ( ( IFile )dirOrFile ).Parent;
            }

            if( !directory.Exists )
            {
                directory.Create();
            }

            var targetFile = directory.File( Name );
            targetFile.WriteAll( ReadAllLines() );

            return targetFile;
        }
    }
}

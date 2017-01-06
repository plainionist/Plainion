using System;
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
                Parent.Create();
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

        public IFile MoveTo( IDirectory directory )
        {
            if( !directory.Exists )
            {
                directory.Create();
            }

            var targetFile = ( FileImpl )directory.File( Name );
            targetFile.WriteAll( this.ReadAllLines().ToArray() );

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
            targetFile.WriteAll( this.ReadAllLines().ToArray() );

            return targetFile;
        }
    }
}

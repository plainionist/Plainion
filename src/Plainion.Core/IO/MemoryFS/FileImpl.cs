using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Plainion.IO.MemoryFS
{
    [DataContract(Namespace = DCNames.NS_MemoryFS)]
    internal class FileImpl : AbstractFileSystemEntry<FileSystemImpl>, IFile
    {
        [DataMember(Name = "ContentV2")]
        private byte[] myContent;

        [DataMember(Name = "Exists")]
        private bool myExists;
        [DataMember(Name = "LastWriteTime")]
        private DateTime myLastWriteTime;
        [DataMember(Name = "LastAccessTime")]
        private DateTime myLastAccessTime;

        [DataMember(Name = "Content")]
        private string mySerializedContentV1;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if(mySerializedContentV1 != null)
            {
                myContent = Encoding.Default.GetBytes(mySerializedContentV1);
                mySerializedContentV1 = null;
            }
        }

        public FileImpl(FileSystemImpl fileSystem, string path)
            : base(fileSystem, path)
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
            if(Exists)
            {
                return;
            }

            if(!Parent.Exists)
            {
                Parent.Create();
            }

            myExists = true;
            myContent = new byte[0];
        }

        public override void Delete()
        {
            myExists = false;
            myContent = null;
        }

        public TextWriter CreateWriter()
        {
            return CreateWriter(Encoding.Default);
        }

        public TextWriter CreateWriter(Encoding encoding)
        {
            CreateOnDemand();

            myLastWriteTime = DateTime.Now;

            return new Writer(this, encoding);
        }

        private void CreateOnDemand()
        {
            if(!Exists)
            {
                Create();
            }
        }

        private class Writer : StreamWriter
        {
            private FileImpl myFile;

            public Writer(FileImpl file, Encoding encoding)
                : base(new MemoryStream(), encoding)
            {
                myFile = file;
            }

            protected override void Dispose(bool disposing)
            {
                if(disposing)
                {
                    Flush();
                    myFile.myContent = ((MemoryStream)this.BaseStream).ToArray();
                }

                base.Dispose(disposing);
            }
        }

        public TextReader CreateReader()
        {
            return CreateReader(Encoding.Default);

        }

        public TextReader CreateReader(Encoding encoding)
        {
            CheckExists();

            myLastAccessTime = DateTime.Now;

            return new StreamReader(new MemoryStream(myContent), encoding);
        }

        private void CheckExists()
        {
            if(!Exists)
            {
                throw new FileNotFoundException(Path);
            }
        }

        public Stream Stream(FileAccess access)
        {
            if(access == FileAccess.Read)
            {
                CheckExists();
                return new MemoryStream(myContent);
            }
            else if(access == FileAccess.ReadWrite)
            {
                CreateOnDemand();
                return new WriterStream(this, myContent);
            }
            else
            {
                CreateOnDemand();
                return new WriterStream(this);
            }
        }

        private class WriterStream : MemoryStream
        {
            private FileImpl myFile;

            public WriterStream(FileImpl file)
            {
                myFile = file;
            }

            public WriterStream(FileImpl file, byte[] content)
                : base(content)
            {
                myFile = file;
            }

            protected override void Dispose(bool disposing)
            {
                if(disposing)
                {
                    myFile.myContent = this.ToArray();
                }

                base.Dispose(disposing);
            }
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

        public IFile MoveTo(IDirectory directory)
        {
            if(!directory.Exists)
            {
                directory.Create();
            }

            var targetFile = (FileImpl)directory.File(Name);
            targetFile.CreateOnDemand();
            targetFile.myContent = new byte[myContent.Length];
            Array.Copy(myContent, targetFile.myContent, myContent.Length);

            Delete();

            return targetFile;
        }

        public IFile CopyTo(IFileSystemEntry dirOrFile, bool overwrite)
        {
            var directory = dirOrFile as IDirectory;
            if(directory == null)
            {
                directory = ((IFile)dirOrFile).Parent;
            }

            if(!directory.Exists)
            {
                directory.Create();
            }

            var targetFile = (FileImpl)directory.File(Name);
            targetFile.CreateOnDemand();
            targetFile.myContent = new byte[myContent.Length];
            Array.Copy(myContent, targetFile.myContent, myContent.Length);

            return targetFile;
        }

        public long Size
        {
            get { return myContent.LongLength; }
        }
    }
}

using System;
using System.IO;
using System.Text;

namespace Plainion.IO.RealFS
{
    internal class FileImpl : AbstractFileSystemEntry<FileSystemImpl>, IFile
    {
        public FileImpl( FileSystemImpl fileSystem, string path )
            : base( fileSystem, path )
        {
        }

        public override bool Exists
        {
            get { return File.Exists( Path ); }
        }

        public override void Create()
        {
            File.Create( Path );
        }

        public override void Delete()
        {
            File.Delete( Path );
        }

        public TextWriter CreateWriter()
        {
            return new StreamWriter( Path );
        }

        public TextWriter CreateWriter( Encoding encoding )
        {
            return new StreamWriter( Path, false, encoding );
        }

        public TextReader CreateReader()
        {
            return new StreamReader( Path );
        }
        public TextReader CreateReader( Encoding encoding )
        {
            return new StreamReader( Path, encoding );
        }

        public override DateTime LastWriteTime
        {
            get { return File.GetLastWriteTime( Path ); }
        }

        public override DateTime LastAccessTime
        {
            get { return File.GetLastAccessTime( Path ); }
        }

        public void WriteAll( params string[] text )
        {
            File.WriteAllLines( Path, text );
        }

        public string[] ReadAllLines()
        {
            return File.ReadAllLines( Path );
        }

        public IFile MoveTo( IDirectory directory )
        {
            if ( !directory.Exists )
            {
                directory.Create();
            }

            var targetFile = directory.File( Name );
            File.Move( Path, targetFile.Path );

            return targetFile;
        }
    }
}

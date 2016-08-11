using System.IO;

namespace Plainion.IO.RealFS
{
    public class FileSystemImpl : IFileSystem
    {
        public IDirectory Directory( string path )
        {
            return new DirectoryImpl( this, path );
        }

        public IFile File( string path )
        {
            return new FileImpl( this, path );
        }

        public IFile GetTempFile()
        {
            return new FileImpl( this, Path.GetTempFileName() );
        }

        public IDirectory GetTempPath()
        {
            return new DirectoryImpl( this, Path.GetTempPath() );
        }
    }
}

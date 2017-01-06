using System.IO;

namespace Plainion.IO
{
    /// <summary>
    /// Abstraction interface to filesystem IO.
    /// </summary>
    public interface IFileSystem
    {
        IDirectory Directory( string path );
        IFile File( string path );

        IFile GetTempFile();
        IDirectory GetTempPath();
    }
}

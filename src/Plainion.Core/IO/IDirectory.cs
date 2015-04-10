using System.Collections.Generic;
using System.IO;

namespace Plainion.IO
{
    /// <summary>
    /// Abstraction interface for a directory
    /// </summary>
    public interface IDirectory : IFileSystemEntry
    {
        IFile File( string filename );
        IDirectory Directory( string directory );

        IEnumerable<IFile> GetFiles();
        IEnumerable<IFile> GetFiles( string pattern );
        IEnumerable<IFile> GetFiles( string pattern, SearchOption option );

        void Delete( bool recursive );
    }
}

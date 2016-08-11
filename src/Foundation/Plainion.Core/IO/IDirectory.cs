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

        /// <summary>
        /// Returns an iterator to all files in that directory.
        /// </summary>
        IEnumerable<IFile> EnumerateFiles();

        /// <summary>
        /// Returns an iterator to all files in that directory matching the given wildcard pattern.
        /// </summary>
        IEnumerable<IFile> EnumerateFiles( string pattern );

        /// <summary>
        /// Returns an iterator to all files in that directory matching the given wildcard pattern and optionally searches recursively.
        /// </summary>
        IEnumerable<IFile> EnumerateFiles( string pattern, SearchOption option );

        void Delete( bool recursive );
    }
}

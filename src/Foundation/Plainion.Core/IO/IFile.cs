using System.IO;
using System.Text;

namespace Plainion.IO
{
    /// <summary>
    /// Abstraction interface for a file
    /// </summary>
    public interface IFile : IFileSystemEntry
    {
        TextWriter CreateWriter();
        TextWriter CreateWriter( Encoding encoding );

        TextReader CreateReader();
        TextReader CreateReader( Encoding encoding );

        void WriteAll( params string[] text );

        string[] ReadAllLines();

        /// <summary>
        /// Moves this file to the given target directory.
        /// Returns an instance pointing to the new target file.
        /// </summary>
        IFile MoveTo( IDirectory directory );

        /// <summary>
        /// Copies the given file to the given directory or target file.
        /// Returns an instance pointing to the new target file.
        /// </summary>
        IFile CopyTo( IFileSystemEntry dirOrFile, bool overwrite );
    }
}

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

        IFile MoveTo( IDirectory directory );
    }
}

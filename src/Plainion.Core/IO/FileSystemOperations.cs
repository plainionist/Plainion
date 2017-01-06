using System.Collections.Generic;

namespace Plainion.IO
{
    /// <summary>
    /// Provides extensions to <see cref="IFile"/>, <see cref="IDirectory"/> and <see cref="IFileSystem"/> which implement operations
    /// which can be applied to all impelemenations of IFileSystem.
    /// </summary>
    public static class FileSystemOperations
    {
        public static void WriteAll( this IFile self, params string[] text )
        {
            Contract.RequiresNotNull( self, "self" );

            using( var writer = self.CreateWriter() )
            {
                foreach( var line in text )
                {
                    writer.WriteLine( line );
                }
            }
        }

        public static IList<string> ReadAllLines( this IFile self )
        {
            Contract.RequiresNotNull( self, "self" );

            var lines = new List<string>();
            using( var reader = self.CreateReader() )
            {
                while( true )
                {
                    var line = reader.ReadLine();
                    if( line == null )
                    {
                        break;
                    }
                    lines.Add( line );
                }
            }

            return lines;
        }

        /// <summary>
        /// Returns an iterator to all files in this directory.
        /// </summary>
        public static IEnumerable<IFile> EnumerateFiles( this IDirectory self )
        {
            Contract.RequiresNotNull( self, "self" );

            return self.EnumerateFiles( "*", System.IO.SearchOption.TopDirectoryOnly );
        }

        /// <summary>
        /// Returns an iterator to all files in this directory matching the given wildcard pattern.
        /// </summary>
        public static IEnumerable<IFile> EnumerateFiles( this IDirectory self, string pattern )
        {
            Contract.RequiresNotNull( self, "self" );

            return self.EnumerateFiles( pattern, System.IO.SearchOption.TopDirectoryOnly );
        }
    }
}

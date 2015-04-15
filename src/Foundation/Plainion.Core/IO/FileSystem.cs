using System.IO;

namespace Plainion.IO
{
    public static class FileSystem
    {
        /// <summary>
        /// Copies the given file to the given directory.
        /// </summary>
        public static void CopyTo( string file, string dirOrFile )
        {
            if( Directory.Exists( dirOrFile ) )
            {
                dirOrFile = Path.Combine( dirOrFile, Path.GetFileName( file ) );
            }

            File.Copy( file, dirOrFile, true );
        }

        public static string UnifyPath( this IFileSystem self, string path )
        {
            var unifiedPath = path;

            while( unifiedPath.EndsWith( "/" ) || unifiedPath.EndsWith( "\\" ) )
            {
                unifiedPath = unifiedPath.Substring( 0, unifiedPath.Length - 1 );
            }

            if( unifiedPath.Length > 2 )
            {
                // dont call this with drive letter only because it will
                // turn it into uppercase letter and then it will be treated as different path
                unifiedPath = Path.GetFullPath( unifiedPath );
            }

            return unifiedPath;
        }
    }
}

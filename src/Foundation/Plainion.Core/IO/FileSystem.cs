using System.IO;

namespace Plainion.IO
{
    /// <summary>
    /// Provides filesystem IO related convenience APIs.
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// Unifies the given path in the way that simple string compare succeeds.
        /// Removes trailing (back)slashes and converts into full qualified path.
        /// </summary>
        public static string UnifyPath( string path )
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

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
            if( path.Length == 2 && char.IsLetter( path[ 0 ] ) && path[ 1 ] == ':' )
            {
                // if we call Path.GetFullPath() with drive letter only ("d:") then it returns 
                // current working directory instead 
                // -> just return what we got
                return path;
            }
            else
            {
                var unifiedPath = Path.GetFullPath( path );

                while( unifiedPath.EndsWith( "/" ) || unifiedPath.EndsWith( "\\" ) )
                {
                    unifiedPath = unifiedPath.Substring( 0, unifiedPath.Length - 1 );
                }

                return unifiedPath;
            }
        }
    }
}

using System.Collections;
using System.Linq;
using System.Text;

namespace Plainion.Diagnostics
{
    public static class Enumerable
    {
        /// <summary>
        /// Dumps the given list into a string in human readable format.
        /// </summary>
        public static string ToHuman( this IEnumerable list )
        {
            if ( list == null )
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append( "[" );
            sb.Append( string.Join( "|", list.Cast<object>().Select( a => a.ToString() ).ToArray() ) );
            sb.Append( "]" );

            return sb.ToString();
        }
    }
}

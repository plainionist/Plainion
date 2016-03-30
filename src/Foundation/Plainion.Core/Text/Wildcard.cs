using System.Text.RegularExpressions;

namespace Plainion.Text
{
    /// <summary>
    /// Represents a wildcard running on the
    /// <see cref="System.Text.RegularExpressions"/> engine.
    /// </summary>
    public class Wildcard : Regex
    {
        public Wildcard( string pattern )
            : base( WildcardToRegex( pattern ) )
        {
        }

        /// <summary>
        /// Initializes a wildcard with the given search pattern and options.
        /// </summary>
        public Wildcard( string pattern, RegexOptions options )
            : base( WildcardToRegex( pattern ), options )
        {
        }

        public static string WildcardToRegex( string pattern )
        {
            return "^" + Regex.Escape( pattern ).
                Replace( "\\*", ".*" ).
                Replace( "\\?", "." ) + "$";
        }

#pragma warning disable 108
        public static bool IsMatch( string input, string pattern )
        {
            return new Wildcard( pattern ).IsMatch( input );
        }
#pragma warning restore 108
    }
}

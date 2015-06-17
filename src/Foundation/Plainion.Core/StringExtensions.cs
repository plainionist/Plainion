using System;
using System.Text;

namespace Plainion
{
    /// <summary>
    /// Extension methods to System.String
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if the string contains a "true value"
        /// </summary>
        /// <returns>true if the given string equals (ignoring case): "y", "yes", "on" or "true"</returns>
        public static bool IsTrue( this string value )
        {
            if( value == null )
            {
                return false;
            }

            return ( value.Equals( "true", StringComparison.OrdinalIgnoreCase ) ||
                value.Equals( "y", StringComparison.OrdinalIgnoreCase ) ||
                value.Equals( "yes", StringComparison.OrdinalIgnoreCase ) ||
                value.Equals( "on", StringComparison.OrdinalIgnoreCase ) );
        }

        /// <summary>
        /// String.Contains() implementation supporting StringComparison which allows "contains" checks
        /// with ignoring case.
        /// </summary>
        public static bool Contains( this string source, string value, StringComparison comparison )
        {
            return source.IndexOf( value, comparison ) >= 0;
        }

        /// <summary>
        /// Returns a string with all characters matching the given predicate removed.
        /// </summary>
        public static string RemoveAll( this string str, Func<char, bool> predicate )
        {
            var sb = new StringBuilder();

            foreach( var c in str.ToCharArray() )
            {
                if( !predicate( c ) )
                {
                    sb.Append( c );
                }
            }

            return sb.ToString();
        }
    }
}

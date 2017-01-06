using System;
using System.Collections.Generic;
using System.Linq;

namespace Plainion
{
    /// <summary>
    /// Provides simple but expressive Design-By-Contract convenience APIs.
    /// </summary>
    public static class Contract
    {
        /// <summary>
        /// Requires that the given condition related to method parameters is true.
        /// </summary>
        /// <exception cref="ArgumentException">if condition is not met</exception>
        public static void Requires( bool condition, string format, params object[] args )
        {
            if ( !condition )
            {
                throw new ArgumentException( string.Format( format, args ) );
            }
        }

        /// <summary>
        /// Requires that the given argument is not null.
        /// </summary>
        /// <exception cref="ArgumentNullException">if argument is null</exception>
        public static void RequiresNotNull(object argument, string message)
        {
            if ( argument == null )
            {
                throw new ArgumentNullException( message );
            }
        }

        /// <summary>
        /// Requires that the given argument is not null and not empty.
        /// </summary>
        /// <exception cref="ArgumentNullException">if argument is null or empty</exception>
        public static void RequiresNotNullNotEmpty(string str, string argumentName)
        {
            if ( string.IsNullOrEmpty( str ) )
            {
                throw new ArgumentNullException( "string must not null or empty: " + argumentName );
            }
        }

        /// <summary>
        /// Requires that the given argument is not null, not empty and not only consists of whitespaces.
        /// </summary>
        /// <exception cref="ArgumentNullException">if argument is null, empty or consists only of whitespaces</exception>
        public static void RequiresNotNullNotWhitespace(string str, string argumentName)
        {
            if ( string.IsNullOrWhiteSpace( str ) )
            {
                throw new ArgumentNullException( "string must not null, not empty and not only consist of whitespaces: " + argumentName );
            }
        }

        /// <summary>
        /// Requires that the given argument is not null and not empty.
        /// </summary>
        /// <exception cref="ArgumentNullException">if argument is null</exception>
        /// <exception cref="ArgumentException">if argument is empty</exception>
        public static void RequiresNotNullNotEmpty<T>(IEnumerable<T> collection, string argumentName)
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( argumentName );
            }
            if ( !collection.Any() )
            {
                throw new ArgumentException( "Collection must not be empty: " + argumentName );
            }
        }

        /// <summary>
        /// Requires that the given condition related to inner state of the class is true.
        /// </summary>
        /// <exception cref="InvalidOperationException">if condition is not met</exception>
        public static void Invariant(bool condition, string format, params object[] args)
        {
            if ( !condition )
            {
                throw new InvalidOperationException( string.Format( format, args ) );
            }
        }
    }
}

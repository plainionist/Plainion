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
        public static void Requires( bool condition, string format, params object[] args )
        {
            if ( !condition )
            {
                throw new ArgumentException( string.Format( format, args ) );
            }
        }

        public static void RequiresNotNull( object argument, string message )
        {
            if ( argument == null )
            {
                throw new ArgumentNullException( message );
            }
        }

        public static void RequiresNotNullNotEmpty( string str, string argumentName )
        {
            if ( string.IsNullOrEmpty( str ) )
            {
                throw new ArgumentNullException( "string must not null or empty: " + argumentName );
            }
        }

        public static void RequiresNotNullNotWhitespace( string str, string argumentName )
        {
            if ( string.IsNullOrWhiteSpace( str ) )
            {
                throw new ArgumentNullException( "string must not null, not empty and not only consist of whitespaces: " + argumentName );
            }
        }

        public static void RequiresNotNullNotEmpty<T>( IEnumerable<T> collection, string argumentName )
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

        public static void Invariant( bool condition, string format, params object[] args )
        {
            if ( !condition )
            {
                throw new InvalidOperationException( string.Format( format, args ) );
            }
        }
    }
}

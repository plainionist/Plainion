using System;
using System.Collections.Generic;
using Plainion;

namespace Plainion.Collections
{
    /// <summary>
    /// Additional extensions to generic IEnumerable interface.
    /// </summary>
    public static class IEnumerableTExtensions
    {
        public static Queue<T> ToQueue<T>( this IEnumerable<T> list )
        {
            Contract.RequiresNotNull( list, "list" );

            return new Queue<T>( list );
        }

        public static int IndexOf<T>( this IEnumerable<T> list, Func<T, bool> pred )
        {
            int i = 0;
            foreach ( var item in list )
            {
                if ( pred( item ) )
                {
                    return i;
                }
                ++i;
            }

            return -1;
        }

        public static int IndexOf<T>( this IEnumerable<T> list, T element )
        {
            int i = 0;
            foreach ( var item in list )
            {
                if ( item.Equals( element ) )
                {
                    return i;
                }
                ++i;
            }

            return -1;
        }
    }
}

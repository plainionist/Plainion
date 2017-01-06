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
        /// <summary>
        /// Returns a copy of the given collection as queue.
        /// </summary>
        public static Queue<T> ToQueue<T>( this IEnumerable<T> list )
        {
            Contract.RequiresNotNull( list, "list" );

            return new Queue<T>( list );
        }

        /// <summary>
        /// Returns the first position of the element specified with the given predicate.
        /// Returns -1 if no such element could be found.
        /// </summary>
        public static int IndexOf<T>( this IEnumerable<T> list, Func<T, bool> predicate )
        {
            int i = 0;
            foreach ( var item in list )
            {
                if ( predicate( item ) )
                {
                    return i;
                }
                ++i;
            }

            return -1;
        }

        /// <summary>
        /// Returns the first position of the specified element.
        /// Returns -1 if no such element could be found.
        /// </summary>
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

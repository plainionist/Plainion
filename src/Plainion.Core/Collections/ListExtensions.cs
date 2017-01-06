using System.Collections.Generic;

namespace Plainion.Collections
{
    /// <summary>
    /// Extensions to the generic IList interface.
    /// </summary>
    public static class IListTExtensions
    {
        /// <summary>
        /// Adds the given range to the list.
        /// </summary>
        public static void AddRange<T>( this IList<T> self, IEnumerable<T> range )
        {
            Contract.RequiresNotNull( self, "self" );
            Contract.RequiresNotNull( range, "range" );

            foreach( var item in range )
            {
                self.Add( item );
            }
        }
    }
}

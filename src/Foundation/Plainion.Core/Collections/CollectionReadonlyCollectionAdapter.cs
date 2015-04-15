using System.Collections.Generic;

namespace Plainion.Collections
{
    /// <summary>
    /// Decorates ICollection interface with IReadOnlyCollection.
    /// Useful if you want to pass ".Values" from dictionary as IReadOnlyCollection
    /// </summary>
    public class CollectionReadonlyCollectionAdapter<T> : IReadOnlyCollection<T>
    {
        private ICollection<T> myCollection;

        public CollectionReadonlyCollectionAdapter( ICollection<T> collection )
        {
            myCollection = collection;
        }

        public int Count { get { return myCollection.Count; } }

        public IEnumerator<T> GetEnumerator()
        {
            return myCollection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return myCollection.GetEnumerator();
        }
    }
}

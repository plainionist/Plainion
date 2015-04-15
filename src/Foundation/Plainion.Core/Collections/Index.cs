using System;
using System.Collections.Generic;

namespace Plainion.Collections
{
    /// <summary>
    /// Key/Value data structure which realizes an "update on read" which means that when a requested value for a 
    /// given key does not exist it is created using the provided value creator.
    /// </summary>
    public class Index<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> myIndex;
        private Func<TKey, TValue> myValueCreator;

        public Index( Func<TKey, TValue> valueCreator )
        {
            myValueCreator = valueCreator;

            myIndex = new Dictionary<TKey, TValue>();
        }

        public TValue this[ TKey key ]
        {
            get
            {
                TValue value;
                if( !myIndex.TryGetValue( key, out value ) )
                {
                    value = myValueCreator( key );
                    myIndex.Add( key, value );
                }

                return value;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return myIndex.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return myIndex.GetEnumerator();
        }

        public IEnumerable<TKey> Keys
        {
            get { return myIndex.Keys; }
        }

        public IEnumerable<TValue> Values
        {
            get { return myIndex.Values; }
        }

        public bool ContainsKey( TKey key )
        {
            return myIndex.ContainsKey( key );
        }

        public bool TryGetValue( TKey key, out TValue value )
        {
            return myIndex.TryGetValue( key, out value );
        }

        public int Count
        {
            get { return myIndex.Count; }
        }

        public void Add( TKey key )
        {
            TValue value;
            if( !myIndex.TryGetValue( key, out value ) )
            {
                value = myValueCreator( key );
                myIndex.Add( key, value );
            }
        }

        public bool Remove( TKey key )
        {
            return myIndex.Remove( key );
        }

        public void Clear()
        {
            myIndex.Clear();
        }
    }
}

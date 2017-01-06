using System.Collections.Generic;
using System.Collections.Specialized;

namespace Plainion.Collections
{
    /// <summary>
    /// Combines <see cref="IEnumerable{T}"/> and <see cref="INotifyCollectionChanged"/> to form a contract that expects both.
    /// </summary>
    public interface IObservableEnumerable<out T> : IEnumerable<T>, INotifyCollectionChanged
    {
    }
}

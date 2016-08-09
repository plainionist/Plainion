using System.Collections.Generic;
using System.Collections.Specialized;

namespace Plainion.Collections
{
    /// <summary>
    /// Combines <see cref="IReadOnlyCollection{T}"/> and <see cref="INotifyCollectionChanged"/> to form a contract that expects both.
    /// </summary>
    public interface IObservableReadOnlyCollection<out T> : IReadOnlyCollection<T>, INotifyCollectionChanged
    {
    }
}

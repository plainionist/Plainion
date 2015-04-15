using System.Collections.Generic;
using System.Collections.Specialized;

namespace Plainion.Collections
{
    public interface IObservableEnumerable<out T> : IEnumerable<T>, INotifyCollectionChanged
    {
    }
}

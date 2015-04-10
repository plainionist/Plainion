using System.Collections.Generic;
using System.Collections.Specialized;

namespace Plainion.Collections
{
    public interface IObservableReadOnlyCollection<out T> : IReadOnlyCollection<T>, INotifyCollectionChanged
    {
    }
}

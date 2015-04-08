using System;

namespace Plainion.Windows.Interactivity.DragDrop
{
    public interface IDragable
    {
        Type DataType { get; }
    }
}

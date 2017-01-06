using System;

namespace Plainion.Windows.Interactivity.DragDrop
{
    public interface IDragable
    {
        /// <summary>
        /// Return null if you dynamicaly want to decide to not allow dragging.
        /// </summary>
        Type DataType { get; }
    }
}

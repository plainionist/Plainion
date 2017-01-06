using System;

namespace Plainion.Windows.Interactivity.DragDrop
{
    public interface IDropable
    {
        string DataFormat { get; }

        bool IsDropAllowed( object data, DropLocation location );

        void Drop( object data, DropLocation location );
    }
}

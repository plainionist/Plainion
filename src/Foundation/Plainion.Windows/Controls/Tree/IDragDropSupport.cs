using System;

namespace Plainion.Windows.Controls.Tree
{
    public interface IDragDropSupport
    {
        bool IsDragAllowed { get; }

        bool IsDropAllowed { get; }
    }
}

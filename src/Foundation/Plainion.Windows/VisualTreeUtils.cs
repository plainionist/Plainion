using System.Windows;
using System.Windows.Media;

namespace Plainion.Windows
{
    static class VisualTreeUtils
    {
        public static T FindParentOfType<T>( this DependencyObject self ) where T : DependencyObject
        {
            var current = self;
            while( self != null && !( self is T ) )
            {
                self = VisualTreeHelper.GetParent( self );
            }
            return ( T )self;
        }
    }
}

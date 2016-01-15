using System.Windows;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity.DragDrop
{
    public class FrameworkElementDropBehavior : Behavior<FrameworkElement>
    {
        private string myDataFormat;

        public FrameworkElementDropBehavior()
        {
            DropAcceptedEffect = DragDropEffects.Move;
        }

        public DragDropEffects DropAcceptedEffect { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AllowDrop = true;

            AssociatedObject.DragEnter += OnDragEnter;
            AssociatedObject.DragOver += OnDragOver;
            AssociatedObject.DragLeave += OnDragLeave;
            AssociatedObject.Drop += OnDrop;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.DragEnter -= OnDragEnter;
            AssociatedObject.DragOver -= OnDragOver;
            AssociatedObject.DragLeave -= OnDragLeave;
            AssociatedObject.Drop -= OnDrop;

            base.OnDetaching();
        }

        private void OnDrop( object sender, DragEventArgs e )
        {
            if( myDataFormat != null && e.Data.GetDataPresent( myDataFormat ) )
            {
                var target = AssociatedObject as IDropable ?? AssociatedObject.DataContext as IDropable;
                target.Drop( e.Data.GetData( myDataFormat ), DropLocation.InPlace );
            }

            e.Handled = true;
        }

        private void OnDragLeave( object sender, DragEventArgs e )
        {
            e.Handled = true;
        }

        private void OnDragOver( object sender, DragEventArgs e )
        {
            e.Effects = DragDropEffects.None;

            if( myDataFormat != null && e.Data.GetDataPresent( myDataFormat ) )
            {
                var target = AssociatedObject as IDropable ?? AssociatedObject.DataContext as IDropable;
                if (target.IsDropAllowed(e.Data.GetData(myDataFormat), DropLocation.InPlace))
                {
                    e.Effects = DropAcceptedEffect;
                }
            }

            e.Handled = true;
        }

        private void OnDragEnter( object sender, DragEventArgs e )
        {
            //if the DataContext implements IDropable, record the data type that can be dropped
            if( myDataFormat == null )
            {
                var target = AssociatedObject as IDropable ?? AssociatedObject.DataContext as IDropable;
                if (target != null)
                {
                    myDataFormat = target.DataFormat;
                }
            }

            e.Handled = true;
        }
    }
}

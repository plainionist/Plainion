using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity
{
    public class OpenLinkOnCtrlClickBehavior : Behavior<TextBox>
    {
        private string myLastTooltipUrl;
        private ToolTip myToolTip;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewMouseDown += OnPreviewMouseDown;
            AssociatedObject.PreviewMouseMove += OnPreviewMouseMove;
            AssociatedObject.MouseLeave +=OnMouseLeave;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseDown -= OnPreviewMouseDown;

            base.OnDetaching();
        }

        private void OnPreviewMouseMove( object sender, MouseEventArgs e )
        {
            if ( myToolTip != null )
            {
                myToolTip.IsOpen = false;
            }

            e.Handled = true;

            var url = GetUrlUnderMouse();
            if ( url == null )
            {
                return;
            }

            if ( myLastTooltipUrl == url )
            {
                myToolTip.IsOpen = true;
                return;
            }

            var content = new TextBlock();
            content.Inlines.Add( url );
            content.Inlines.Add( " " );
            content.Inlines.Add( new Bold( new Run( "Open with Ctrl-Click" ) ) );

            if ( myToolTip == null )
            {
                myToolTip = new ToolTip();
            }

            myToolTip.Content = content;
            myToolTip.IsOpen = true;

            AssociatedObject.ToolTip = myToolTip;

            myLastTooltipUrl = url;
        }

        private void OnMouseLeave( object sender, MouseEventArgs e )
        {
            if( myToolTip != null )
            {
                myToolTip.IsOpen = false;
            }
        }

        private void OnPreviewMouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( e.ChangedButton != MouseButton.Left )
            {
                return;
            }

            if ( e.ClickCount != 1 )
            {
                return;
            }

            if ( ( Keyboard.Modifiers & ModifierKeys.Control ) != ModifierKeys.Control )
            {
                return;
            }

            e.Handled = true;

            var url = GetUrlUnderMouse();
            if ( url == null )
            {
                return;
            }

            try
            {
                Process.Start( url );
            }
            catch
            {
                // ignore all exceptions here
            }
        }

        private string GetUrlUnderMouse()
        {
            var mousePoint = Mouse.GetPosition( AssociatedObject );
            int charPosition = AssociatedObject.GetCharacterIndexFromPoint( mousePoint, true );
            if ( charPosition < 0 )
            {
                return null;
            }

            int index = 0;
            int i = 0;
            string[] strings = AssociatedObject.Text.Split( new string[] { " ", "\t", "\n" }, StringSplitOptions.None );
            while ( index + strings[ i ].Length < charPosition && i < strings.Length )
            {
                index += strings[ i++ ].Length + 1;
            }

            return strings[ i ];
        }
    }
}

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Practices.Prism.Interactivity;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Plainion.Prism.Interactivity
{
    [DefaultProperty( "WindowContent" ), ContentProperty( "WindowContent" )]
    public class PopupViewAction : PopupWindowAction
    {
        public static readonly DependencyProperty WindowWidthProperty = DependencyProperty.Register(
            "WindowWidth", typeof( double? ), typeof( PopupViewAction ), new PropertyMetadata( null ) );

        public double? WindowWidth
        {
            get { return ( double? )GetValue( WindowWidthProperty ); }
            set { SetValue( WindowWidthProperty, value ); }
        }

        public static readonly DependencyProperty WindowHeightProperty = DependencyProperty.Register(
            "WindowHeight", typeof( double? ), typeof( PopupViewAction ), new PropertyMetadata( null ) );

        public double? WindowHeight
        {
            get { return ( double? )GetValue( WindowHeightProperty ); }
            set { SetValue( WindowHeightProperty, value ); }
        }

        public static readonly DependencyProperty ResizeModeProperty = DependencyProperty.Register(
            "ResizeMode", typeof( ResizeMode ), typeof( PopupViewAction ), new PropertyMetadata( ResizeMode.CanResize ) );

        public ResizeMode ResizeMode
        {
            get { return ( ResizeMode )GetValue( ResizeModeProperty ); }
            set { SetValue( ResizeModeProperty, value ); }
        }

        public static readonly DependencyProperty UseNotificationContentAsDataContextProperty = DependencyProperty.Register(
            "UseNotificationContentAsDataContext", typeof( bool ), typeof( PopupViewAction ), new PropertyMetadata( null ) );

        public bool UseNotificationContentAsDataContext
        {
            get { return ( bool )GetValue( UseNotificationContentAsDataContextProperty ); }
            set { SetValue( UseNotificationContentAsDataContextProperty, value ); }
        }

        protected override Window GetWindow( INotification notification )
        {
            var window = base.GetWindow( notification );

            if( WindowWidth.HasValue )
            {
                window.Width = WindowWidth.Value;
            }

            if( WindowHeight.HasValue )
            {
                window.Height = WindowHeight.Value;
            }

            window.ResizeMode = ResizeMode;

            if( WindowWidth.HasValue || WindowHeight.HasValue )
            {
                // unfortunately we cannot tell base class to NOT set SizeToContent so we correct it afterwards
                var d = DependencyPropertyDescriptor.FromProperty( Window.SizeToContentProperty, typeof( Window ) );
                d.AddValueChanged( window, OnSizeToContentChanged );
            }

            if( UseNotificationContentAsDataContext )
            {
                window.DataContext = notification.Content;
            }

            window.Closed += OnWindowClosed;

            return window;
        }

        private void OnSizeToContentChanged( object sender, EventArgs e )
        {
            var window = ( Window )sender;

            if( WindowWidth.HasValue && WindowHeight.HasValue )
            {
                window.SizeToContent = SizeToContent.Manual;
            }
            else if( WindowWidth.HasValue )
            {
                window.SizeToContent = SizeToContent.Height;
            }
            else
            {
                window.SizeToContent = SizeToContent.Width;
            }
        }

        private void OnWindowClosed( object sender, EventArgs e )
        {
            var window = ( Window )sender;
            
            var d = DependencyPropertyDescriptor.FromProperty( Window.SizeToContentProperty, typeof( Window ) );
            d.RemoveValueChanged( window, OnSizeToContentChanged );

            window.Closed -= OnWindowClosed;
        }
    }
}

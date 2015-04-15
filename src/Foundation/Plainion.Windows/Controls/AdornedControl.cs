using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Plainion.Windows.Controls
{
    /// <remarks>
    /// Initial version taken from: 
    /// - http://www.codeproject.com/Articles/57984/WPF-Loading-Wait-Adorner
    /// - http://www.codeproject.com/Articles/54472/Defining-WPF-Adorners-in-XAML
    /// </remarks>
    public class AdornedControl : ContentControl
    {
        // Caches the adorner layer.
        private AdornerLayer myAdornerLayer;

        // The actual adorner create to contain our 'adorner UI content'.
        private FrameworkElementAdorner myAdorner;

        public AdornedControl()
        {
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
        {
            UpdateAdornerDataContext();
        }

        private void UpdateAdornerDataContext()
        {
            if( AdornerContent != null )
            {
                AdornerContent.DataContext = DataContext;
            }
        }
        
        public static readonly DependencyProperty IsAdornerVisibleProperty =
            DependencyProperty.Register( "IsAdornerVisible", typeof( bool ), typeof( AdornedControl ),
                new FrameworkPropertyMetadata( OnIsAdornerVisibleChanged ) );

        private static void OnIsAdornerVisibleChanged( DependencyObject o, DependencyPropertyChangedEventArgs e )
        {
            AdornedControl c = ( AdornedControl )o;
            c.ApplyAdornerVisibility();
        }

        public bool IsAdornerVisible
        {
            get { return ( bool )GetValue( IsAdornerVisibleProperty ); }
            set { SetValue( IsAdornerVisibleProperty, value ); }
        }

        public static readonly DependencyProperty AdornerContentProperty =
            DependencyProperty.Register( "AdornerContent", typeof( FrameworkElement ), typeof( AdornedControl ),
                new FrameworkPropertyMetadata( OnAdornerContentChanged ) );

        private static void OnAdornerContentChanged( DependencyObject o, DependencyPropertyChangedEventArgs e )
        {
            AdornedControl c = ( AdornedControl )o;
            c.ApplyAdornerVisibility();
        }
        
        public FrameworkElement AdornerContent
        {
            get { return ( FrameworkElement )GetValue( AdornerContentProperty ); }
            set { SetValue( AdornerContentProperty, value ); }
        }

        public static readonly DependencyProperty HorizontalAdornerPlacementProperty =
            DependencyProperty.Register( "HorizontalAdornerPlacement", typeof( AdornerPlacement ), typeof( AdornedControl ),
                new FrameworkPropertyMetadata( AdornerPlacement.Inside ) );

        public AdornerPlacement HorizontalAdornerPlacement
        {
            get { return ( AdornerPlacement )GetValue( HorizontalAdornerPlacementProperty ); }
            set { SetValue( HorizontalAdornerPlacementProperty, value ); }
        }

        public static readonly DependencyProperty VerticalAdornerPlacementProperty =
            DependencyProperty.Register( "VerticalAdornerPlacement", typeof( AdornerPlacement ), typeof( AdornedControl ),
                new FrameworkPropertyMetadata( AdornerPlacement.Inside ) );

        public AdornerPlacement VerticalAdornerPlacement
        {
            get { return ( AdornerPlacement )GetValue( VerticalAdornerPlacementProperty ); }
            set { SetValue( VerticalAdornerPlacementProperty, value ); }
        }

        public static readonly DependencyProperty AdornerOffsetXProperty =
            DependencyProperty.Register( "AdornerOffsetX", typeof( double ), typeof( AdornedControl ) );

        public double AdornerOffsetX
        {
            get { return ( double )GetValue( AdornerOffsetXProperty ); }
            set { SetValue( AdornerOffsetXProperty, value ); }
        }

        public static readonly DependencyProperty AdornerOffsetYProperty =
            DependencyProperty.Register( "AdornerOffsetY", typeof( double ), typeof( AdornedControl ) );

        public double AdornerOffsetY
        {
            get { return ( double )GetValue( AdornerOffsetYProperty ); }
            set { SetValue( AdornerOffsetYProperty, value ); }
        }

        private void ApplyAdornerVisibility()
        {
            if( IsAdornerVisible )
            {
                ShowAdorner();
            }
            else
            {
                HideAdorner();
            }
        }

        private void ShowAdorner()
        {
            if( myAdorner != null )
            {
                return;
            }

            if( AdornerContent != null )
            {
                if( myAdornerLayer == null )
                {
                    myAdornerLayer = AdornerLayer.GetAdornerLayer( this );
                }

                if( myAdornerLayer != null )
                {
                    myAdorner = new FrameworkElementAdorner( AdornerContent, this, HorizontalAdornerPlacement, VerticalAdornerPlacement,
                                                     AdornerOffsetX, AdornerOffsetY );
                    myAdornerLayer.Add( myAdorner );

                    UpdateAdornerDataContext();
                }
            }
        }

        private void HideAdorner()
        {
            if( myAdornerLayer == null || myAdorner == null )
            {
                return;
            }

            myAdornerLayer.Remove( myAdorner );
            myAdorner.DisconnectChild();

            myAdorner = null;
            myAdornerLayer = null;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ApplyAdornerVisibility();
        }
    }
}

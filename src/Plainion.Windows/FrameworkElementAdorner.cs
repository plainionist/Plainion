using System;
using System.Collections;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Plainion.Windows
{
    /// <summary>
    /// This class is an adorner that allows a FrameworkElement derived class to adorn another FrameworkElement.
    /// </summary>
    /// <remarks>
    /// Initial version taken from: http://www.codeproject.com/Articles/57984/WPF-Loading-Wait-Adorner
    /// which was inspired by: http://www.codeproject.com/KB/WPF/WPFJoshSmith.aspx
    /// </remarks>
    public class FrameworkElementAdorner : Adorner
    {
        private FrameworkElement myChild;
        private AdornerPlacement mxHorizontalAdornerPlacement = AdornerPlacement.Inside;
        private AdornerPlacement myVerticalAdornerPlacement = AdornerPlacement.Inside;
        private double myOffsetX = 0.0;
        private double myOffsetY = 0.0;
        private double myPositionX = Double.NaN;
        private double myPositionY = Double.NaN;

        public FrameworkElementAdorner( FrameworkElement adornerChildElement, FrameworkElement adornedElement )
            : base( adornedElement )
        {
            myChild = adornerChildElement;

            base.AddLogicalChild( adornerChildElement );
            base.AddVisualChild( adornerChildElement );
        }

        public FrameworkElementAdorner( FrameworkElement adornerChildElement, FrameworkElement adornedElement,
                                       AdornerPlacement horizontalAdornerPlacement, AdornerPlacement verticalAdornerPlacement,
                                       double offsetX, double offsetY )
            : base( adornedElement )
        {
            myChild = adornerChildElement;
            mxHorizontalAdornerPlacement = horizontalAdornerPlacement;
            myVerticalAdornerPlacement = verticalAdornerPlacement;
            myOffsetX = offsetX;
            myOffsetY = offsetY;

            adornedElement.SizeChanged += OnAdornedElementSizeChanged;

            base.AddLogicalChild( adornerChildElement );
            base.AddVisualChild( adornerChildElement );
        }

        private void OnAdornedElementSizeChanged( object sender, SizeChangedEventArgs e )
        {
            InvalidateMeasure();
        }

        //
        // Position of the child (when not set to NaN).
        //
        public double PositionX
        {
            get
            {
                return myPositionX;
            }
            set
            {
                myPositionX = value;
            }
        }

        public double PositionY
        {
            get
            {
                return myPositionY;
            }
            set
            {
                myPositionY = value;
            }
        }

        protected override Size MeasureOverride( Size constraint )
        {
            myChild.Measure( constraint );
            return myChild.DesiredSize;
        }

        /// <summary>
        /// Determine the X coordinate of the child.
        /// </summary>
        private double DetermineX()
        {
            switch( myChild.HorizontalAlignment )
            {
                case HorizontalAlignment.Left:
                    {
                        if( mxHorizontalAdornerPlacement == AdornerPlacement.Outside )
                        {
                            return -myChild.DesiredSize.Width + myOffsetX;
                        }
                        else
                        {
                            return myOffsetX;
                        }
                    }
                case HorizontalAlignment.Right:
                    {
                        if( mxHorizontalAdornerPlacement == AdornerPlacement.Outside )
                        {
                            double adornedWidth = AdornedElement.ActualWidth;
                            return adornedWidth + myOffsetX;
                        }
                        else
                        {
                            double adornerWidth = myChild.DesiredSize.Width;
                            double adornedWidth = AdornedElement.ActualWidth;
                            double x = adornedWidth - adornerWidth;
                            return x + myOffsetX;
                        }
                    }
                case HorizontalAlignment.Center:
                    {
                        double adornerWidth = myChild.DesiredSize.Width;
                        double adornedWidth = AdornedElement.ActualWidth;
                        double x = ( adornedWidth / 2 ) - ( adornerWidth / 2 );
                        return x + myOffsetX;
                    }
                case HorizontalAlignment.Stretch:
                    {
                        return 0.0;
                    }
            }

            return 0.0;
        }

        private double DetermineY()
        {
            switch( myChild.VerticalAlignment )
            {
                case VerticalAlignment.Top:
                    {
                        if( myVerticalAdornerPlacement == AdornerPlacement.Outside )
                        {
                            return -myChild.DesiredSize.Height + myOffsetY;
                        }
                        else
                        {
                            return myOffsetY;
                        }
                    }
                case VerticalAlignment.Bottom:
                    {
                        if( myVerticalAdornerPlacement == AdornerPlacement.Outside )
                        {
                            double adornedHeight = AdornedElement.ActualHeight;
                            return adornedHeight + myOffsetY;
                        }
                        else
                        {
                            double adornerHeight = myChild.DesiredSize.Height;
                            double adornedHeight = AdornedElement.ActualHeight;
                            double x = adornedHeight - adornerHeight;
                            return x + myOffsetY;
                        }
                    }
                case VerticalAlignment.Center:
                    {
                        double adornerHeight = myChild.DesiredSize.Height;
                        double adornedHeight = AdornedElement.ActualHeight;
                        double x = ( adornedHeight / 2 ) - ( adornerHeight / 2 );
                        return x + myOffsetY;
                    }
                case VerticalAlignment.Stretch:
                    {
                        return 0.0;
                    }
            }

            return 0.0;
        }

        private double DetermineWidth()
        {
            if( !Double.IsNaN( PositionX ) )
            {
                return myChild.DesiredSize.Width;
            }

            switch( myChild.HorizontalAlignment )
            {
                case HorizontalAlignment.Left:
                    {
                        return myChild.DesiredSize.Width;
                    }
                case HorizontalAlignment.Right:
                    {
                        return myChild.DesiredSize.Width;
                    }
                case HorizontalAlignment.Center:
                    {
                        return myChild.DesiredSize.Width;
                    }
                case HorizontalAlignment.Stretch:
                    {
                        return AdornedElement.ActualWidth;
                    }
            }

            return 0.0;
        }

        private double DetermineHeight()
        {
            if( !Double.IsNaN( PositionY ) )
            {
                return myChild.DesiredSize.Height;
            }

            switch( myChild.VerticalAlignment )
            {
                case VerticalAlignment.Top:
                    {
                        return myChild.DesiredSize.Height;
                    }
                case VerticalAlignment.Bottom:
                    {
                        return myChild.DesiredSize.Height;
                    }
                case VerticalAlignment.Center:
                    {
                        return myChild.DesiredSize.Height;
                    }
                case VerticalAlignment.Stretch:
                    {
                        return AdornedElement.ActualHeight;
                    }
            }

            return 0.0;
        }

        protected override Size ArrangeOverride( Size finalSize )
        {
            double x = PositionX;
            if( Double.IsNaN( x ) )
            {
                x = DetermineX();
            }
            double y = PositionY;
            if( Double.IsNaN( y ) )
            {
                y = DetermineY();
            }
            double adornerWidth = DetermineWidth();
            double adornerHeight = DetermineHeight();
            myChild.Arrange( new Rect( x, y, adornerWidth, adornerHeight ) );
            return finalSize;
        }

        protected override Int32 VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild( Int32 index )
        {
            return myChild;
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                ArrayList list = new ArrayList();
                list.Add( myChild );
                return ( IEnumerator )list.GetEnumerator();
            }
        }

        public void DisconnectChild()
        {
            base.RemoveLogicalChild( myChild );
            base.RemoveVisualChild( myChild );
        }

        public new FrameworkElement AdornedElement
        {
            get
            {
                return ( FrameworkElement )base.AdornedElement;
            }
        }
    }
}
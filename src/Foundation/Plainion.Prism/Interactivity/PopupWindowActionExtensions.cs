using System.Windows;
using Prism.Interactivity;
using Prism.Regions;

namespace Plainion.Prism.Interactivity
{
    public static class PopupWindowActionExtensions
    {
        public static string GetRegionName( DependencyObject obj )
        {
            return ( string )obj.GetValue( RegionNameProperty );
        }

        public static void SetRegionName( DependencyObject obj, string value )
        {
            obj.SetValue( RegionNameProperty, value );
        }

        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached(
             "RegionName", typeof( string ), typeof( PopupWindowActionExtensions ), new UIPropertyMetadata( null, OnRegionNameChanged ) );

        private static void OnRegionNameChanged( DependencyObject target, DependencyPropertyChangedEventArgs e )
        {
            var popupWindowAction = target as PopupWindowAction;

            Contract.Requires( popupWindowAction != null, "Attached property plainion:RegionName is only supported on PopupWindowAction" );

            popupWindowAction.WindowContent = new PopupWindowContentControl();

            RegionManager.SetRegionName( popupWindowAction.WindowContent, ( string )e.NewValue );
        }
    }
}

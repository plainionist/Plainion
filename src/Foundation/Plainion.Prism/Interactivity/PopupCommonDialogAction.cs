using System;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;
using Prism.Interactivity.InteractionRequest;
using Microsoft.Win32;

namespace Plainion.Prism.Interactivity
{
    public class PopupCommonDialogAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty FileDialogTypeProperty =
            DependencyProperty.Register(
                "FileDialogType",
                typeof( Type ),
                typeof( PopupCommonDialogAction ),
                new PropertyMetadata( null ) );

        /// <summary>
        /// Some derived type of Microsoft.Win32.CommonDialog e.g. Microsoft.Win32.OpenFileDialog or Microsoft.Win32.SaveFileDialog
        /// </summary>
        public Type FileDialogType
        {
            get { return ( Type )GetValue( FileDialogTypeProperty ); }
            set { SetValue( FileDialogTypeProperty, value ); }
        }

        protected override void Invoke( object parameter )
        {
            var args = parameter as InteractionRequestedEventArgs;
            if( args == null )
            {
                return;
            }

            if( FileDialogType == null )
            {
                throw new ArgumentException( "FileDialogType not set" );
            }

            var dialog = ( CommonDialog )Activator.CreateInstance( FileDialogType );

            // bind from ViewModel to View
            foreach( var prop in args.Context.GetType().GetProperties() )
            {
                var value = prop.GetValue( args.Context, null );
                if( value == null )
                {
                    // ignore unset values
                    continue;
                }

                var dialogProp = dialog.GetType().GetProperties().FirstOrDefault( p => p.Name.Equals( prop.Name, StringComparison.OrdinalIgnoreCase ) );
                if( dialogProp != null && dialogProp.CanWrite )
                {
                    dialogProp.SetValue( dialog, value, null );
                }
            }

            var ret = dialog.ShowDialog();

            // bind from View to ViewModel
            foreach( var prop in dialog.GetType().GetProperties() )
            {
                var ctxProp = args.Context.GetType().GetProperties().FirstOrDefault( p => p.Name.Equals( prop.Name, StringComparison.OrdinalIgnoreCase ) );
                if( ctxProp != null && ctxProp.CanWrite )
                {
                    ctxProp.SetValue( args.Context, prop.GetValue( dialog, null ), null );
                }
            }

            var configmation = args.Context as IConfirmation;
            if( configmation != null )
            {
                configmation.Confirmed = ret == true;
            }

            args.Callback();
        }
    }
}

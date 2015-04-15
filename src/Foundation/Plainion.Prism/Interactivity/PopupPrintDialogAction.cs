using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Plainion.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Plainion.Prism.Interactivity
{
    public class PopupPrintDialogAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty PrintSourceProperty = DependencyProperty.Register(
          "PrintSource", typeof( IPrintRequestAware ), typeof( PopupPrintDialogAction ), new PropertyMetadata( null ) );

        public IPrintRequestAware PrintSource
        {
            get { return ( IPrintRequestAware )GetValue( PrintSourceProperty ); }
            set { SetValue( PrintSourceProperty, value ); }
        }

        protected override void Invoke( object parameter )
        {
            var args = parameter as InteractionRequestedEventArgs;
            if( args == null )
            {
                return;
            }

            var printSource = GetPrintSource();
            if( printSource == null )
            {
                return;
            }

            var dialog = new PrintDialog();
            var ret = dialog.ShowDialog();

            if( ret == true )
            {
                var printSize = new Size( dialog.PrintableAreaWidth, dialog.PrintableAreaHeight );
                var paginator = printSource.GetPaginator( printSize );

                dialog.PrintTicket.PageOrientation = printSource.PreferredOrientation;

                try
                {
                    dialog.PrintDocument( paginator, args.Context.Title );
                }
                catch( PrintSystemException x )
                {
                    MessageBox.Show( x.Message, args.Context.Title );
                }
            }

            var configmation = args.Context as IConfirmation;
            if( configmation != null )
            {
                configmation.Confirmed = ret == true;
            }

            args.Callback();
        }

        private IPrintRequestAware GetPrintSource()
        {
            var printAware = PrintSource as IPrintRequestAware;
            if( printAware != null )
            {
                return printAware;
            }

            printAware = AssociatedObject as IPrintRequestAware;
            if( printAware != null )
            {
                return printAware;
            }

            return AssociatedObject.DataContext as IPrintRequestAware;
        }
    }
}

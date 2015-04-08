using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity
{
    public class RaiseCommandOnMouseGestureBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
             "Command", typeof( ICommand ), typeof( RaiseCommandOnMouseGestureBehavior ) );

        public ICommand Command
        {
            get { return ( ICommand )GetValue( CommandProperty ); }
            set { SetValue( CommandProperty, value ); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
             "CommandParameter", typeof( object ), typeof( RaiseCommandOnMouseGestureBehavior ) );

        public object CommandParameter
        {
            get { return GetValue( CommandParameterProperty ); }
            set { SetValue( CommandParameterProperty, value ); }
        }

        public static readonly DependencyProperty ModifiersProperty = DependencyProperty.Register(
             "Modifiers", typeof( ModifierKeys ), typeof( RaiseCommandOnMouseGestureBehavior ) );

        public ModifierKeys Modifiers
        {
            get { return ( ModifierKeys )GetValue( ModifiersProperty ); }
            set { SetValue( ModifiersProperty, value ); }
        }

        public static readonly DependencyProperty MouseButtonProperty = DependencyProperty.Register(
             "MouseButton", typeof( MouseButton ), typeof( RaiseCommandOnMouseGestureBehavior ) );

        public MouseButton MouseButton
        {
            get { return ( MouseButton )GetValue( MouseButtonProperty ); }
            set { SetValue( MouseButtonProperty, value ); }
        }

        public static readonly DependencyProperty ClickCountProperty = DependencyProperty.Register(
             "ClickCount", typeof( int ), typeof( RaiseCommandOnMouseGestureBehavior ) );

        public int ClickCount
        {
            get { return ( int )GetValue( ClickCountProperty ); }
            set { SetValue( ClickCountProperty, value ); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewMouseDown += OnPreviewMouseDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseDown -= OnPreviewMouseDown;

            base.OnDetaching();
        }

        private void OnPreviewMouseDown( object sender, MouseButtonEventArgs e )
        {
            if( Command == null || !Command.CanExecute( CommandParameterProperty ) )
            {
                return;
            }

            if( e.ChangedButton == MouseButton && e.ClickCount == ClickCount && Keyboard.Modifiers == Modifiers )
            {
                Command.Execute( CommandParameterProperty );
                e.Handled = true;
            }
        }
    }
}

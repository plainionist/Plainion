using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Plainion.Windows.Controls
{
    [TemplatePart( Name = "PART_TextBlock", Type = typeof( EditableTextBlock ) )]
    [TemplatePart( Name = "PART_TextBox", Type = typeof( EditableTextBlock ) )]
    public class EditableTextBlock : Control, INotifyPropertyChanged
    {
        private string myTextBeforeEdit;
        private TextBlock myTextBlock;
        private TextBox myTextBox;

        static EditableTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( EditableTextBlock ), new FrameworkPropertyMetadata( typeof( EditableTextBlock ) ) );
        }

        public EditableTextBlock()
        {
            Focusable = true;
            FocusVisualStyle = null;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            myTextBlock = ( TextBlock )Template.FindName( "PART_TextBlock", this );

            if( myTextBox != null )
            {
                myTextBox.LostFocus -= OnTextBoxFocusLost;
                myTextBox.KeyDown -= OnTextBoxKeyDown;
            }

            myTextBox = ( TextBox )Template.FindName( "PART_TextBox", this );
            myTextBox.LostFocus += OnTextBoxFocusLost;
            myTextBox.KeyDown += OnTextBoxKeyDown;

            OnIsInEditModeChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register( "Text", typeof( string ),
            typeof( EditableTextBlock ), new FrameworkPropertyMetadata( null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged ) );

        private static void OnTextChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
           ((EditableTextBlock)d).RaiseFormattedTextChanged();
        }

        public string Text
        {
            get { return ( string )GetValue( TextProperty ); }
            set { SetValue( TextProperty, value ); }
        }

        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register( "IsEditable", typeof( bool ),
            typeof( EditableTextBlock ), new PropertyMetadata( true ) );

        public bool IsEditable
        {
            get { return ( bool )GetValue( IsEditableProperty ); }
            set { SetValue( IsEditableProperty, value ); }
        }

        public static readonly DependencyProperty IsInEditModeProperty = DependencyProperty.Register( "IsInEditMode", typeof( bool ),
            typeof( EditableTextBlock ), new PropertyMetadata( false, OnIsInEditModeChanged, CoerceIsInEditModeChanged ) );

        private static void OnIsInEditModeChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( EditableTextBlock )d ).OnIsInEditModeChanged();
        }

        private void OnIsInEditModeChanged()
        {
            if( myTextBlock == null )
            {
                // if we are initially Visiblity.Collapsed then OnApplyTemplate() is not yet called.
                // we consider current IsInEditMode during OnApplyTemplate()
                return;
            }

            if( Visibility != Visibility.Visible )
            {
                myTextBlock.Visibility = Visibility.Collapsed;
                myTextBox.Visibility = Visibility.Collapsed;
                return;
            }

            if( IsInEditMode )
            {
                myTextBeforeEdit = Text;

                myTextBlock.Visibility = Visibility.Collapsed;
                myTextBox.Visibility = Visibility.Visible;

                myTextBox.Focus();
                myTextBox.SelectAll();
            }
            else
            {
                myTextBlock.Visibility = Visibility.Visible;
                myTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private static object CoerceIsInEditModeChanged( DependencyObject d, object baseValue )
        {
            var self = ( EditableTextBlock )d;

            return self.IsEditable ? baseValue : false;
        }

        public bool IsInEditMode
        {
            get { return ( bool )GetValue( IsInEditModeProperty ); }
            set { SetValue( IsInEditModeProperty, value ); }
        }

        public static readonly DependencyProperty TextFormatProperty = DependencyProperty.Register( "TextFormat", typeof( string ),
            typeof( EditableTextBlock ), new PropertyMetadata( "{0}" ) );

        public string TextFormat
        {
            get { return ( string )GetValue( TextFormatProperty ); }
            set
            {
                if( string.IsNullOrWhiteSpace( value ) )
                {
                    value = "{0}";
                }

                SetValue( TextFormatProperty, value );
            }
        }

        public string FormattedText
        {
            get { return string.Format( TextFormat, Text ); }
        }

        private void OnTextBoxFocusLost( object sender, RoutedEventArgs e )
        {
            IsInEditMode = false;
        }

        private void OnTextBoxKeyDown( object sender, KeyEventArgs e )
        {
            // Attention: always first sync text back to view model - only then adjust IsInEditMode
            // usecase: there might be some temp text added by user - e.g. "enter text here" or s.th.

            if( e.Key == Key.Enter )
            {
                var binding = myTextBox.GetBindingExpression( TextBox.TextProperty );
                binding.UpdateSource();

                RaiseFormattedTextChanged();

                IsInEditMode = false;
                e.Handled = true;
            }
            else if( e.Key == Key.Escape )
            {
                myTextBox.Text = myTextBeforeEdit;

                IsInEditMode = false;
                e.Handled = true;
            }
        }

        private void RaiseFormattedTextChanged()
        {
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( "FormattedText" ) );
            }
        }
    }
}

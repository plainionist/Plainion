using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;

namespace Plainion.RI.InteractionRequests
{
    /// <summary>
    /// This sample does not provide any "WindowContent" to the PopupWindowAction. PopupWindowAction will then create a "default" window and 
    /// assign the notification as DataContext. The default window contains a ContentControl which binds INotification.Content to its Content property.
    /// </summary>
    [Export]
    class DefaultConfirmationViewModel : BindableBase
    {
        private string myResponse;

        public DefaultConfirmationViewModel()
        {
            ShowConfirmationViewCommand = new DelegateCommand( OnShowConfirmationView );
            ShowConfirmationViewModelCommand = new DelegateCommand( OnShowConfirmationViewModel );
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }

        public ICommand ShowConfirmationViewCommand { get; private set; }

        private void OnShowConfirmationView()
        {
            var confirmation = new Confirmation();
            confirmation.Title = "Really?";

            // applying "view" elements directly here wouldnt be the prefered approach but at least it is simple
            confirmation.Content = new Border
            {
                BorderBrush = Brushes.Green,
                BorderThickness = new System.Windows.Thickness( 2 ),
                Padding = new System.Windows.Thickness( 10 ),
                Child = new TextBlock { Text = "Here goes the question, doesn't it?" }
            };

            ConfirmationRequest.Raise( confirmation, c => Response = c.Confirmed ? "yes" : "no" );
        }

        public ICommand ShowConfirmationViewModelCommand { get; private set; }

        private void OnShowConfirmationViewModel()
        {
            var confirmation = new Confirmation();
            confirmation.Title = "Really?";
            // we assume there is a DataTemplate somewhere which will "style" this content
            confirmation.Content = new ConfirmationContent("Here goes the question, doesn't it?");

            ConfirmationRequest.Raise( confirmation, c => Response = c.Confirmed ? "yes" : "no" );
        }

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; private set; }

        public string Response
        {
            get { return myResponse; }
            set { SetProperty( ref myResponse, value ); }
        }
    }
}

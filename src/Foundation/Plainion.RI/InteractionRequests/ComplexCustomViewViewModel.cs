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
    /// This sample provides a custom UserControl to  PopupWindowAction.WindowContent with a custom viewmodel. Within this sample different
    /// approaches how to connect the ViewModel with the views DataContext are discussed.
    /// <para>
    /// This approach works well for complex dialogs like "Settings" dialogs.
    /// </para>
    /// </summary>
    [Export]
    class ComplexCustomViewViewModel : BindableBase
    {
        public ComplexCustomViewViewModel()
        {
            // of course in real application we would NOT create the model here but import it from MEF
            Model = new Model();

            ShowConfirmationCommand = new DelegateCommand( OnShowConfirmation );
            ConfirmationRequest = new InteractionRequest<INotification>();
        }

        public Model Model { get; private set; }

        public ICommand ShowConfirmationCommand { get; private set; }

        // Provides the viewmodel for the dialog as notification content. Of course we could import the viewmodel from MEF as well
        // in order to avoid passing dependencies manually but still we have to know the concrete type of the viewmodel of the dialog.
        private void OnShowConfirmation()
        {
            var notification = new Notification();
            notification.Title = "Really?";

            // DOES NOT WORK
            // - we fail to update the DataContext to Notification.Content without logic in code behind of view
            // - we fail to update the DataContext of the view in a way that PopupWindowAction considers viewmodel for check of IInteractionRequestAware
            notification.Content = new ComplexCustomView_DialogViewModel( Model );

            ConfirmationRequest.Raise( notification, n => { } );
        }

        public InteractionRequest<INotification> ConfirmationRequest { get; private set; }
    }

    [Export]
    public class ComplexCustomView_DialogViewModel : BindableBase, IInteractionRequestAware
    {
        private Model myModel;

        /// <summary>
        /// Usually we can assume that for complex UI we have a complex ViewModel with dependencies.
        /// Ideally we let MEF create it in order to resolve dependencies automatically.
        /// </summary>
        [ImportingConstructor]
        public ComplexCustomView_DialogViewModel( Model model )
        {
            myModel = model;

            ApplyCommand = new DelegateCommand( OnApply );
            OkCommand = new DelegateCommand( OnOk );
            CancelCommand = new DelegateCommand( OnCancel );
        }

        public ICommand ApplyCommand { get; private set; }

        private void OnApply()
        {
            myModel.JustMySampleState = "Apply";
        }

        public ICommand OkCommand { get; private set; }

        private void OnOk()
        {
            myModel.JustMySampleState = "Ok";
            FinishInteraction();
        }

        public ICommand CancelCommand { get; private set; }

        private void OnCancel()
        {
            FinishInteraction();
        }

        public Action FinishInteraction { get; set; }

        public INotification Notification { get; set; }
    }

    public class Model : BindableBase
    {
        private string myJustMySampleState;

        public string JustMySampleState
        {
            get { return myJustMySampleState; }
            set { SetProperty( ref myJustMySampleState, value ); }
        }
    }
}

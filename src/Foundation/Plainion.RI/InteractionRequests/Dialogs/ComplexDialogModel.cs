using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;

namespace Plainion.RI.InteractionRequests.Dialogs
{
    [Export, PartCreationPolicy( CreationPolicy.NonShared )]
    public class ComplexDialogModel : BindableBase, IInteractionRequestAware
    {
        private Model myModel;

        /// <summary>
        /// Usually we can assume that for complex UI we have a complex ViewModel with dependencies.
        /// Ideally we let MEF create it in order to resolve dependencies automatically.
        /// </summary>
        [ImportingConstructor]
        public ComplexDialogModel( Model model )
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
            myModel.JustMySampleState = "Cancel";
            FinishInteraction();
        }

        public Action FinishInteraction { get; set; }

        public INotification Notification { get; set; }
    }
}

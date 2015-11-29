using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Plainion.RI.InteractionRequests
{
    [Export]
    public partial class ComplexCustomView_DialogView : UserControl
    {
        public ComplexCustomView_DialogView()
        {
            InitializeComponent();

            // we have to create and pass the viewmodel outside because of the viewmodel's dependencies the view
            // cannot just create it here
        }

        /// <summary>
        /// Usually of course we would have only one constructor but in order to keep the sample small ...
        /// </summary>
        [ImportingConstructor]
        internal ComplexCustomView_DialogView( ComplexCustomView_DialogViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

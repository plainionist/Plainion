using System.Windows;

namespace Plainion.RI
{
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();

            DataContext = new ShellViewModel();
        }
    }
}

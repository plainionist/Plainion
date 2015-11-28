using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Plainion.Windows.Controls;

namespace Plainion.RI
{
    class ShellViewModel : BindableBase
    {
        public ShellViewModel()
        {
            SelectFolderCommand = new DelegateCommand( OnSelectFolder );
        }

        public ICommand SelectFolderCommand { get; private set; }

        private void OnSelectFolder()
        {
            var dialog = new SelectFolderDialog();
            dialog.Description = "Select some folder you like";
            if( dialog.ShowDialog() ==true)
            {
                MessageBox.Show( "Selected folder: " + dialog.SelectedPath );
            }
        }
    }
}

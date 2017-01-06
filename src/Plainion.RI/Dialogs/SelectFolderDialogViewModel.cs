using System.ComponentModel.Composition;
using System.Windows.Input;
using Plainion.Windows.Controls;
using Prism.Commands;
using Prism.Mvvm;

namespace Plainion.RI.Dialogs
{
    [Export]
    class SelectFolderDialogViewModel : BindableBase
    {
        private string mySelectedFolder;

        public SelectFolderDialogViewModel()
        {
            SelectFolderCommand = new DelegateCommand( OnSelectFolder );
        }

        public ICommand SelectFolderCommand { get; private set; }

        private void OnSelectFolder()
        {
            var dialog = new SelectFolderDialog();
            dialog.Description = "Select some folder you like";
            if( dialog.ShowDialog() == true )
            {
                SelectedFolder = dialog.SelectedPath;
            }
        }

        public string SelectedFolder
        {
            get { return mySelectedFolder; }
            set { SetProperty( ref mySelectedFolder, value ); }
        }
    }
}

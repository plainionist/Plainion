using System;
using System.Windows.Forms;

namespace Plainion.Windows.Controls
{
    /// <summary>
    /// Provides WPF wrapper for System.Windows.Forms.FolderBrowserDialog. It derives from <see cref="CommonDialog"/> in order
    /// to support same handling as <see cref="OpenFileDialog"/> and <see cref="SaveFileDialog"/>.
    /// </summary>
    public class SelectFolderDialog : Microsoft.Win32.CommonDialog
    {
        /// <summary>
        /// Gets or sets the descriptive text displayed above the tree view control in the dialog box.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the root folder where the browsing starts from.
        /// </summary>
        public Environment.SpecialFolder RootFolder { get; set; }

        /// <summary>
        /// Gets or sets the path selected by the user.
        /// </summary>
        public string SelectedPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the New Folder button appears in the folder browser dialog box.
        /// </summary>
        public bool ShowNewFolderButton { get; set; }

        /// <summary>
        /// Resets properties to their default values.
        /// </summary>
        public override void Reset()
        {
            Description = null;
            RootFolder = Environment.SpecialFolder.Desktop;
            SelectedPath = null;
            ShowNewFolderButton = false;
        }

        protected override bool RunDialog( IntPtr hwndOwner )
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = Description;
            folderBrowserDialog.RootFolder = RootFolder;
            folderBrowserDialog.SelectedPath = SelectedPath;
            folderBrowserDialog.ShowNewFolderButton = ShowNewFolderButton;

            var result = folderBrowserDialog.ShowDialog( new WindowWrapper( hwndOwner ) );

            SelectedPath = folderBrowserDialog.SelectedPath;

            return result == DialogResult.OK || result == DialogResult.Yes;
        }

        private class WindowWrapper : IWin32Window
        {
            public WindowWrapper( IntPtr handle )
            {
                Handle = handle;
            }

            public IntPtr Handle { get; private set; }
        }
    }
}

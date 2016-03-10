using System;
using Prism.Interactivity.InteractionRequest;

namespace Plainion.Prism.Interactivity.InteractionRequest
{
    public class SelectFolderDialogNotification : Confirmation
    {
        /// <see cref="P:Plainion.Windows.Controls.SelectFolderDialog.Description"/>
        public string Description { get; set; }

        /// <see cref="P:Plainion.Windows.Controls.SelectFolderDialog.RootFolder"/>
        public Environment.SpecialFolder RootFolder { get; set; }

        /// <see cref="P:Plainion.Windows.Controls.SelectFolderDialog.SelectedPath"/>
        public string SelectedPath { get; set; }

        /// <see cref="P:Plainion.Windows.Controls.SelectFolderDialog.ShowNewFolderButton"/>
        public bool ShowNewFolderButton { get; set; }

    }
}

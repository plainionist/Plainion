
namespace Plainion.Controls.Interactivity.InteractionRequest
{
    public class OpenFileDialogNotification : FileDialogNotificationBase
    {
        /// <see cref="P:Microsoft.Win32.FileDialog.MultiSelect"/>
        public bool? MultiSelect
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.ReadOnlyChecked"/>
        public bool? ReadOnlyChecked
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.ShowReadOnly"/>
        public bool? ShowReadOnly
        {
            get;
            set;
        }
    }
}


namespace Plainion.Controls.Interactivity.InteractionRequest
{
    public class SaveFileDialogNotification : FileDialogNotificationBase
    {
        /// <see cref="P:Microsoft.Win32.FileDialog.CreatePrompt"/>
        public bool? CreatePrompt
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.OverwritePrompt"/>
        public bool? OverwritePrompt
        {
            get;
            set;
        }
    }
}

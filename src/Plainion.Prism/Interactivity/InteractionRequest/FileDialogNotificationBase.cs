using System.Collections.Generic;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Win32;

namespace Plainion.Controls.Interactivity.InteractionRequest
{
    /// <summary>
    /// Notification base class for Microsoft.Win32.FileDialog.
    /// </summary>
    public class FileDialogNotificationBase : Confirmation
    {
        protected FileDialogNotificationBase()
        {
            CustomPlaces = new List<FileDialogCustomPlace>();
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.Filer"/>
        public string Filter
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.FileName"/>
        public string FileName
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.AddExtension"/>
        public bool? AddExtension
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.CheckFileExists"/>
        public bool? CheckFileExists
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.CheckPathExists"/>
        public bool? CheckPathExists
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.CustomPlaces"/>
        public IList<FileDialogCustomPlace> CustomPlaces
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.DefaultExt"/>
        public string DefaultExt
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.DereferenceLinks"/>
        public bool? DereferenceLinks
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.FileNames"/>
        public string[] FileNames
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.FilterIndex"/>
        public int? FilterIndex
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.InitialDirectory"/>
        public string InitialDirectory
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.SafeFileName"/>
        public string SafeFileName
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.SafeFileNames"/>
        public string[] SafeFileNames
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.ValidateNames"/>
        public bool? ValidateNames
        {
            get;
            set;
        }

        /// <see cref="P:Microsoft.Win32.FileDialog.RestoreDirectory"/>
        public bool? RestoreDirectory
        {
            get;
            set;
        }
    }
}

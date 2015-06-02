using System.IO;
using Microsoft.Practices.Prism.Mvvm;

namespace Plainion.AppFw.Wpf.Infrastructure
{
    public class ProjectBase : BindableBase
    {
        private string myLocation;
        private bool myIsDirty;

        public ProjectBase()
        {
            IsDirty = false;
        }

        public string Location
        {
            get { return myLocation; }
            set
            {
                var location = value != null ? Path.GetFullPath( value ) : null;

                if( myLocation == location )
                {
                    return;
                }

                myLocation = location;

                OnLocationChanged();
            }
        }

        protected virtual void OnLocationChanged()
        {
            OnPropertyChanged( "Location" );
        }

        public bool IsDirty
        {
            get { return myIsDirty; }
            set { SetProperty( ref myIsDirty, value ); }
        }
    }
}

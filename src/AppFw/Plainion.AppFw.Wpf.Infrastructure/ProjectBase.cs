using System.IO;
using Prism.Mvvm;

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

        /// <summary>
        /// The location can be empty for new projects and can be changed later on 
        /// but follows write-once policy.
        /// </summary>
        public string Location
        {
            get { return myLocation; }
            set
            {
                Contract.Invariant( myLocation == null, "Location can be initialized only once" );

                Contract.RequiresNotNullNotEmpty( value, "value" );

                var location = value != null ? Path.GetFullPath( value ) : null;

                myLocation = location;

                OnLocationChanged();
            }
        }

        /// <summary>
        /// Provided by derived classes to adjust properties derived from Location.
        /// A notification that the location has changed does not mean that the file is already saved at this 
        /// location. therefore the "IsDirty" flag has to considered as well.
        /// </summary>
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

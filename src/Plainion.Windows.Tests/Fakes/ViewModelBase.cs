using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Plainion.Windows.Tests.Fakes
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>( ref T storage, T value, [CallerMemberName] string propertyName = null )
        {
            if( object.Equals( storage, value ) )
            {
                return false;
            }

            storage = value;

            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
            }

            return true;
        }
    }
}

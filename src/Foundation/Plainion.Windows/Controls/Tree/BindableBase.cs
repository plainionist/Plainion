using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Plainion.Windows.Controls.Tree
{
    public class BindableBase : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            return false;
        }
    }
}


namespace Plainion.Windows.Tests.Fakes
{
    class ViewModel1 : ViewModelBase
    {
        private int myValue;

        public int PrimaryValue
        {
            get { return myValue; }
            set { SetProperty( ref myValue, value ); }
        }
    }
}

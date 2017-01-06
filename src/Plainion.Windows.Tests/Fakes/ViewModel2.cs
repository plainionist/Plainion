
namespace Plainion.Windows.Tests.Fakes
{
    class ViewModel2 : ViewModelBase
    {
        private int myValue;

        public int SecondaryValue
        {
            get { return myValue; }
            set { SetProperty( ref myValue, value ); }
        }
    }
}

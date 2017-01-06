
namespace Plainion.Windows.Tests.Fakes
{
    class Model : ViewModelBase
    {
        private int myValue;

        public int ModelValue
        {
            get { return myValue; }
            set { SetProperty( ref myValue, value ); }
        }
    }
}

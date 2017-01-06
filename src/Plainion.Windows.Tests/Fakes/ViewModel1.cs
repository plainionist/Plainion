
namespace Plainion.Windows.Tests.Fakes
{
    class ViewModel1 : ViewModelBase
    {
        private int myValue;

        public ViewModel1()
        {
            Model = new Model();
        }

        public Model Model { get; private set; }

        public int PrimaryValue
        {
            get { return myValue; }
            set { SetProperty( ref myValue, value ); }
        }
    }
}

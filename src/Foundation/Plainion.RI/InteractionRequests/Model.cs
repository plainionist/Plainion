using Microsoft.Practices.Prism.Mvvm;

namespace Plainion.RI.InteractionRequests
{
    public class Model : BindableBase
    {
        private string myJustMySampleState;

        public string JustMySampleState
        {
            get { return myJustMySampleState; }
            set { SetProperty( ref myJustMySampleState, value ); }
        }
    }
}

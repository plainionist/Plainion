using System.ComponentModel.Composition;
using Prism.Mvvm;

namespace Plainion.RI.InteractionRequests
{
    [Export]
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

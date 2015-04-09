using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Plainion.Prism.Interactivity.InteractionRequest
{
    public class Dialog : Confirmation, IDialog
    {
        public double? Width { get; set; }
        public double? Height { get; set; }
    }
}

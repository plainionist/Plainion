using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Plainion.Controls.Interactivity.InteractionRequest
{
    interface IDialog : IConfirmation
    {
        double? Width { get; set; }
        double? Height { get; set; }
    }
}

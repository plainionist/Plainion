
namespace Plainion.RI.InteractionRequests
{
    class ConfirmationContent
    {
        public ConfirmationContent( string question )
        {
            Question = question;
        }

        public string Question { get; private set; }
    }
}

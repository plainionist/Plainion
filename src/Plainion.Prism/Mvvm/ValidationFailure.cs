
namespace Plainion.Controls.Mvvm
{
    public class ValidationFailure
    {
        public ValidationFailure( Severity severity )
            : this( severity, string.Empty )
        {
        }

        public ValidationFailure( Severity severity, string message )
        {
            Severity = severity;
            Message = message;
        }

        public Severity Severity
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }

        public static ValidationFailure Warning = new ValidationFailure( Severity.Warning );
        public static ValidationFailure Error = new ValidationFailure( Severity.Error );
    }
}

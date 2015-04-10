
namespace Plainion.Logging
{
    public interface ILoggingSink
    {
        void Write( ILogEntry entry );
    }
}

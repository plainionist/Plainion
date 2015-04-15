
namespace Plainion.Logging
{
    public interface ILogEntry
    {
        LogLevel Level { get; }
        string Message { get; }
    }
}

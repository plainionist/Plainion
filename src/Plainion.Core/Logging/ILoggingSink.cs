
namespace Plainion.Logging
{
    /// <summary>
    /// Defines a sink which actually writes the message e.g. to the console, a file or into a window.
    /// </summary>
    /// <remarks>
    /// Implementations may need to get informed about the start and end of the logging session. Such triggers need to be provided by
    /// the application directly to the implementation. <seealso cref="FileLoggingSink"/>
    /// </remarks>
    public interface ILoggingSink
    {
        void Write( ILogEntry entry );
    }
}

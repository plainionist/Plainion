using Plainion.Logging;

namespace Plainion.Tests.Logging
{
    class FakeLoggingSink : ILoggingSink
    {
        public ILogEntry LastEntry { get; private set; }

        public void Write( ILogEntry entry )
        {
            LastEntry = entry;
        }
    }
}

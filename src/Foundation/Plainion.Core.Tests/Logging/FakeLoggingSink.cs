using Plainion.Logging;

namespace Plainion.Core.Tests.Logging
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

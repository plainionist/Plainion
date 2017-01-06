using System.IO;
using System.Linq;
using NUnit.Framework;
using Plainion.Logging;

namespace Plainion.Tests.Logging
{
    [TestFixture]
    class FileLoggingSinkTests
    {
        [Test]
        public void Write_WhenCalled_EntryWrittenWithTimestamp()
        {
            var logFile = Path.GetTempFileName();
            var sink = new FileLoggingSink( logFile );

            sink.Open();

            var entry = new LogEntry( LogLevel.Warning, "test" );
            sink.Write( entry );

            sink.Close();

            var lines = File.ReadAllLines( logFile );

            Assert.That( lines.Single(), Is.EqualTo( string.Format( "{0}|Warning|test", entry.Timestamp ) ) );

            File.Delete( logFile );
        }
    }
}

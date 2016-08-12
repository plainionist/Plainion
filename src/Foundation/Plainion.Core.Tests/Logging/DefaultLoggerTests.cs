using System;
using NUnit.Framework;
using Plainion.Logging;

namespace Plainion.Core.Tests.Logging
{
    [TestFixture]
    class DefaultLoggerTests
    {
        [Test]
        public void Debug_WhenCalled_LogLevelIsDebug()
        {
            var sink = new FakeLoggingSink();

            new DefaultLogger( sink ).Debug( "some message" );

            Assert.That( sink.LastEntry.Level, Is.EqualTo( LogLevel.Debug ) );
        }

        [Test]
        public void Info_WhenCalled_LogLevelIsInfo()
        {
            var sink = new FakeLoggingSink();

            new DefaultLogger( sink ).Info( "some message" );

            Assert.That( sink.LastEntry.Level, Is.EqualTo( LogLevel.Info ) );
        }

        [Test]
        public void Notice_WhenCalled_LogLevelIsNotice()
        {
            var sink = new FakeLoggingSink();

            new DefaultLogger( sink ).Notice( "some message" );

            Assert.That( sink.LastEntry.Level, Is.EqualTo( LogLevel.Notice ) );
        }

        [Test]
        public void Warning_WhenCalled_LogLevelIsWarning()
        {
            var sink = new FakeLoggingSink();

            new DefaultLogger( sink ).Warning( "some message" );

            Assert.That( sink.LastEntry.Level, Is.EqualTo( LogLevel.Warning ) );
        }

        [Test]
        public void Error_WhenCalled_LogLevelIsError()
        {
            var sink = new FakeLoggingSink();

            new DefaultLogger( sink ).Error( "some message" );

            Assert.That( sink.LastEntry.Level, Is.EqualTo( LogLevel.Error ) );
        }

        [Test]
        public void Write_LogLevelLessThanConfigured_NoMessageWritten()
        {
            var sink = new FakeLoggingSink();
            var logger = new DefaultLogger( sink );

            DefaultLogger.LogLevel = LogLevel.Warning;
            logger.Write( LogLevel.Info, "some message" );

            Assert.That( sink.LastEntry, Is.Null );
        }

        [Test]
        public void Write_LogLevelEqualToConfigured_MessageWritten()
        {
            var sink = new FakeLoggingSink();
            var logger = new DefaultLogger( sink );

            DefaultLogger.LogLevel = LogLevel.Warning;
            logger.Write( LogLevel.Warning, "some message" );

            Assert.That( sink.LastEntry.Message, Is.EqualTo( "some message" ) );
        }

        [Test]
        public void Write_LogLevelGreaterThanConfigured_MessageWritten()
        {
            var sink = new FakeLoggingSink();
            var logger = new DefaultLogger( sink );

            DefaultLogger.LogLevel = LogLevel.Info;
            logger.Write( LogLevel.Warning, "some message" );

            Assert.That( sink.LastEntry.Message, Is.EqualTo( "some message" ) );
        }

        [Test]
        public void Warning_InvalidFormatStringInException_DoesNotThrow()
        {
            var sink = new FakeLoggingSink();

            new DefaultLogger( sink ).Warning( new Exception( "{abc" ), "test" );

            Assert.That( sink.LastEntry.Message, Is.StringContaining( "{abc" ) );
        }

        [Test]
        public void Error_InvalidFormatStringInException_DoesNotThrow()
        {
            var sink = new FakeLoggingSink();

            new DefaultLogger( sink ).Error( new Exception( "{abc" ), "test" );

            Assert.That( sink.LastEntry.Message, Is.StringContaining( "{abc" ) );
        }
    }
}

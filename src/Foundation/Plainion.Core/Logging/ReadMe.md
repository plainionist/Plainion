
This namespace provides a simple but extensible logging framework.

# Usage

## Using a logger

Create a static member of ILogger in all classes which want to log:

```C#
private static readonly ILogger myLogger = LoggerFactory.GetLogger( typeof( ScriptLoader ) );
```

Use the various methods to log messages of different LogLevels:

```C#
myLogger.Debug( "Processing starter script: {0}", file );
```

## Configure the framework

Choose an appropriate implementation of ILoggerFactoringImpl and set it to the LoggerFactory together with a LogLevel

```C#
LoggerFactory.Implementation = new ConsoleLoggerFactoringImpl();
LoggerFactory.LogLevel = LogLevel.Info;
```

The framework provides the following implementations of ILoggerFactoringImpl

- ConsoleLoggerFactoringImpl : logs all messages to the System.Console
- LoggingSinkLoggerFactory: logs all messages to the ILoggingSink instances added via AddGuiAppender


# Extending the framework

The framework can be extended by implementing ILoggerFactoringImpl which returns custom implementation of ILogger.

Alternatively LoggingSinkLoggerFactory can be used together with a custom implementation of ILoggingSink.


TBD


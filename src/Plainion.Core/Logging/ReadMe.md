
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

The minimum configuration of the framework requires adding an ILoggingSink

```C#
LoggerFactory.AddSink( new ConsoleLoggingSink() );
```

The framework provides the following implementations of ILoggingSink

- ConsoleLoggingSink : logs all messages to the System.Console
- FileLoggingSink: logs all messages to the given file

Usually the default LogLevel should be adjusted according to the needs of the application (default is LogLevel.Warning).

# Extending the framework

The simplest extension of the logging framework is a custom logging sink. Therefore only ILoggingSink needs to be implemented and an instance passed to LoggingFactory.AddSink().
This approach is most appropriate e.g. when writing logging messages into a statusbar or status window of an application.

More advanced extensions of the logging framework require a custom implementation of ILoggingFactory. This approach allows loading logging configuration from some URI and 
implementing a custom ILogger. If only the former option is wanted the DefaultLogger can be reused.

When implementing a custom ILogger even a custom ILoggingEntry can be used which may carry additional application specific context information which could then be used by a 
custom ILoggingSink, e.g.: the source of the message (the class which created the logger). The LoggerBase supports implementing a custom logger.

## Example

see Plainion.Prism repository included "Plainion.RI" projct for a sample statusbar based logging


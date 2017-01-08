
# Plainion.Logging.LoggerBase

**Namespace:** Plainion.Logging

**Assembly:** Plainion.Core

Base class for
*See:* T:Plainion.Logging.ILogger
implementations.


## Constructors

### Constructor()


## Properties

### Plainion.Logging.LogLevel ConfiguredLogLevel

Derived classes must implement this SPI to provide


## Methods

### void Debug(System.String format,System.Object[] args)

### void Info(System.String format,System.Object[] args)

### void Notice(System.String format,System.Object[] args)

### void Warning(System.String format,System.Object[] args)

### void Warning(System.Exception exception,System.String format,System.Object[] args)

### void Error(System.String format,System.Object[] args)

### void Error(System.Exception exception,System.String format,System.Object[] args)

### void WriteCore(Plainion.Logging.LogLevel level,System.String msg)

Called from all log APIs if the configured log level allows it. Derived classes must implement this SPI to perform the actual logging.


# Plainion.Logging.ILoggingSink

**Namespace:** Plainion.Logging

**Assembly:** Plainion.Core

Defines a sink which actually writes the message e.g. to the console, a file or into a window.

## Remarks

Implementations may need to get informed about the start and end of the logging session. Such triggers need to be provided by the application directly to the implementation.

## See also

* Plainion.Logging.FileLoggingSink


## Methods

### void Write(Plainion.Logging.ILogEntry entry)

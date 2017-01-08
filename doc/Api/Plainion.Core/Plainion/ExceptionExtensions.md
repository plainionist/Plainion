
# Plainion.ExceptionExtensions

**Namespace:** Plainion

**Assembly:** Plainion.Core

Provides extension methods to
*See:* T:System.Exception
objects.


## Methods

### System.String Dump(System.Exception exception)

Dumps the given exception content to a string and returns it.

### void DumpTo(System.Exception exception,System.IO.TextWriter writer)

Dumps the given exception content to the given TextWriter.

### void PreserveStackTrace(System.Exception exception)

Preserves the full stack trace before rethrowing an exception.

#### Remarks

According to this post see http://weblogs.asp.net/fmarguerie/archive/2008/01/02/rethrowing-exceptions-and-preserving-the-full-call-stack-trace.aspx it is required to get the full stack trace in any case.

### System.Exception AddContext(System.Exception exception,System.String key,System.Object value)

Adds a key/value pair to Exception.Data

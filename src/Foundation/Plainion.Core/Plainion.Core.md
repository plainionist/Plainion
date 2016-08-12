# Plainion.Core

## IO.FileSystemOperations
Provides extensions to , and which implement operations which can be applied to all impelemenations of IFileSystem.

### Methods

#### EnumerateFiles(Plainion.IO.IDirectory)
Returns an iterator to all files in this directory.

#### EnumerateFiles(Plainion.IO.IDirectory,System.String)
Returns an iterator to all files in this directory matching the given wildcard pattern.

## Logging.FileLoggingSink
Implementation of which logs to a file. It requires calling Open() before the first entry can be written and Close() when the logging session ends.

## Logging.ILoggingSink
Defines a sink which actually writes the message e.g. to the console, a file or into a window.

> ### Remarks

> Implementations may need to get informed about the start and end of the logging session. Such triggers need to be provided by the application directly to the implementation.

## Logging.ConsoleLoggingSink
Implementation of which logs to System.Console in different colors.

## Progress.NullProgress`1
Null-object pattern for IProgress{T}

## Progress.NullProgress
Null-object pattern for IProgress{IProgressInfo}

## Serialization.SerializableBindableBase
Supports INotifyPropertyChanged for model entities.

> ### Remarks

> BindableBase from Prism cannot be used with DataContractSerializer because it does not have DataContractAttribute applied which is mandatory. (Same for BinaryFormatter)

## Xaml.IncludeExtension
Xaml markup comparable to XInclude. Allows simple include of Xaml files into other Xaml files.

## Xaml.ValidatingXamlReader
Reads given Xaml input and validates it using DataAnnotations and

## Collections.CollectionReadonlyCollectionAdapter`1
Decorates ICollection interface with IReadOnlyCollection. Useful if you want to pass ".Values" from dictionary as IReadOnlyCollection

## Collections.IEnumerableTExtensions
Additional extensions to generic IEnumerable interface.

### Methods

#### ToQueue``1(System.Collections.Generic.IEnumerable{``0})
Returns a copy of the given collection as queue.

#### IndexOf``1(System.Collections.Generic.IEnumerable{``0},System.Func{``0,System.Boolean})
Returns the first position of the element specified with the given predicate. Returns -1 if no such element could be found.

#### IndexOf``1(System.Collections.Generic.IEnumerable{``0},``0)
Returns the first position of the specified element. Returns -1 if no such element could be found.

## Collections.IListTExtensions
Extensions to the generic IList interface.

### Methods

#### AddRange``1(System.Collections.Generic.IList{``0},System.Collections.Generic.IEnumerable{``0})
Adds the given range to the list.

## Collections.Index`2
Key/Value data structure which realizes an "update on read" which means that when a requested value for a given key does not exist it is created using the provided value creator.

### Properties

#### Item(`0)

Creates requested value on demand if not exists

### Methods

#### TryGetValue(`0,`1@)
Performs lookup for existing values. No value will be created on demand.

#### Add(`0)
Adds the given key and creates a value using the value creator IF the key does not exist yet.

## Collections.IObservableEnumerable`1
Combines and to form a contract that expects both.

## Collections.IObservableReadOnlyCollection`1
Combines and to form a contract that expects both.

## Composition.Composer
Simplify usage of MEF or plain vanilla usages of it.

## Composition.IComposer
Interface to slightly abstract away MEF CompositionContainer.

## Composition.DecoratorChainCatalog
Provides MEF support for the decorator pattern. This catalog builds a chain of decorators for the contract specified at the constructor and from the types added using Add(). Each decorator type has to import and export the "contract to decorate". The last addd type will be the most outer decorator which will then satisfy imports requiring the "contract to decorate".

> ### Remarks

> Internally this catalog rewrites the contracts of the parts in the chain to avoid conflicts. Recomposition is currently not supported.

> ### Example

>

```

var decoration = new DecoratorChainCatalog( typeof( IContract ) ); decoration.Add( typeof( Impl ) ); decoration.Add( typeof( Decorator1 ) ); decoration.Add( typeof( Decorator2 ) ); // - pass this catalog to MEF CompositionContainer // - everyone importing IContract will get the Decorator2 which decorates Decorator1 which decorates the actual implementation "Impl".
```

### Methods

#### Constructor
Creates a catalog for the contract specified by the given type.

#### Constructor
Creates a catalog for the contract specified by the given contract name.

## Contract
Provides simple but expressive Design-By-Contract convenience APIs.

### Methods

#### Requires(System.Boolean,System.String,System.Object[])
Requires that the given condition related to method parameters is true.

> ##### Exceptions

> **System.ArgumentException:**  if condition is not met

#### RequiresNotNull(System.Object,System.String)
Requires that the given argument is not null.

> ##### Exceptions

> **System.ArgumentNullException:**  if argument is null

#### RequiresNotNullNotEmpty(System.String,System.String)
Requires that the given argument is not null and not empty.

> ##### Exceptions

> **System.ArgumentNullException:**  if argument is null or empty

#### RequiresNotNullNotWhitespace(System.String,System.String)
Requires that the given argument is not null, not empty and not only consists of whitespaces.

> ##### Exceptions

> **System.ArgumentNullException:**  if argument is null, empty or consists only of whitespaces

#### RequiresNotNullNotEmpty``1(System.Collections.Generic.IEnumerable{``0},System.String)
Requires that the given argument is not null and not empty.

> ##### Exceptions

> **System.ArgumentNullException:**  if argument is null

> **System.ArgumentException:**  if argument is empty

#### Invariant(System.Boolean,System.String,System.Object[])
Requires that the given condition related to inner state of the class is true.

> ##### Exceptions

> **System.InvalidOperationException:**  if condition is not met

## Diagnostics.ProcessThreadIndex`1
Provides an for process-thread trees

## Diagnostics.ProcessThreadSet
Provides an for process to thread collections

## ExceptionExtensions
Provides extension methods to objects.

### Methods

#### Dump(System.Exception)
Dumps the given exception content to a string and returns it.

#### DumpTo(System.Exception,System.IO.TextWriter)
Dumps the given exception content to the given TextWriter.

#### DumpTo(System.Exception,System.IO.TextWriter,System.Int32)
Dumps the given exception content to the given TextWriter. Content will be: exception type, message, exception.Data, StackTrace. InnerException is handled recursively. ReflectionTypeLoadException is expanded and LoaderExceptions are dummed.

#### PreserveStackTrace(System.Exception)
Preserves the full stack trace before rethrowing an exception. According to this post see http://weblogs.asp.net/fmarguerie/archive/2008/01/02/rethrowing-exceptions-and-preserving-the-full-call-stack-trace.aspx it is required to get the full stack trace in any case.

#### AddContext(System.Exception,System.String,System.Object)
Adds a key/value pair to Exception.Data

## IO.AbstractFileSystemEntry`1
Base class for filesystem entries.

> ### Remarks

> Comparision is based on "Path" respecting case!

## IO.IFileSystemEntry
Abstraction interface for common functionallity of a file and a directory.

## IO.DCNames
Internally used to serialize the MemoryFS

## IO.FileSystem
Provides filesystem IO related convenience APIs.

### Methods

#### UnifyPath(System.String)
Unifies the given path in the way that simple string compare succeeds. Removes trailing (back)slashes and converts into full qualified path.

## IO.IDirectory
Abstraction interface for a directory

### Methods

#### EnumerateFiles(System.String,System.IO.SearchOption)
Returns an iterator to all files in that directory matching the given wildcard pattern and optionally searches recursively.

## IO.IFile
Abstraction interface for a file

### Methods

#### MoveTo(Plainion.IO.IDirectory)
Moves this file to the given target directory. Returns an instance pointing to the new target file.

#### CopyTo(Plainion.IO.IFileSystemEntry,System.Boolean)
Copies the given file to the given directory or target file. Returns an instance pointing to the new target file.

## IO.IFileSystem
Abstraction interface to filesystem IO.

## Diagnostics.Processes
Provides convenience APIs to launch external programs.

### Methods

#### Execute(System.Diagnostics.ProcessStartInfo)
Executes a process and waits for its exit.

#### Execute(System.Diagnostics.ProcessStartInfo,System.IO.TextWriter,System.IO.TextWriter)
Executes a process and waits for its exit.

## Logging.CompositeLoggingSink
Implements composite pattern for

## Logging.ILogEntry
Defines the minimum contract of a log entry which can be handled by

## Logging.ILogger
Defines the APIs provided by the logging framework to its clients which wants to log messages.

## Logging.ILoggerFactory
Main interface of the logging framework. Provides APIs for configuration as well as creation of instances.

## Logging.LoggerFactory
Facade for the actual logger factory implementation. Provides simple access to the logging framework as kind of singleton.

## Logging.DefaultLogger
Default implementation of .

### Methods

#### Write(Plainion.Logging.LogLevel,System.String,System.Object[])
Logs the given message if the given logging fits the current logging level.

## Logging.DefaultLoggerFactory
Default implementation of which allows adding different kinds of s.

### Methods

#### LoadConfiguration(System.Uri)
Does NOT load any configuration from the given uri. Just sets the default LogLevel to LogLevel.Warning

## NumberExtensions
Extensions for numeric values to increase usability.

### Methods

#### Times(System.Int32,System.Action{System.Int32})
Supports Ruby like syntax for a number or repetitions (for loops). 5.Times( i => Console.WriteLine( "." ) );

## Progress.CountingProgress
Defines a progress which starts at 1 and just increases without defined maximum.

## Progress.IProgressInfo
Provides simple progress description to be used with System.IProgress

### Properties

#### Activity

The activity going on which progress is reported.

#### Progress

Progress value.

#### Details

Additional details describing the activity or steps of it.

## Progress.PercentageProgress
Defines a progress which gives percentage updates.

## Progress.UndefinedProgress
Defines a progress without value. This implementation is usually used if a progress from a third party async activity should be reported where detailed updates are not provided but a description can be given to the user.

## StringExtensions
Extension methods to System.String

### Methods

#### IsTrue(System.String)
Returns true if the string contains a "true value"

> ##### Return value

> true if the given string equals (ignoring case): "y", "yes", "on" or "true"

#### Contains(System.String,System.String,System.StringComparison)
String.Contains() implementation supporting StringComparison which allows "contains" checks with ignoring case.

#### RemoveAll(System.String,System.Func{System.Char,System.Boolean})
Returns a string with all characters matching the given predicate removed.

## Tasks.Tasks
Provides extensions to the TPL.

### Methods

#### StartSTATask``1(System.Func{``0})
Starts a STA thread wrapped by a Task.

#### StartSTATask(System.Action)
Starts a STA thread wrapped by a Task.

## Text.Wildcard
Represents a wildcard running on the engine.

### Methods

#### Constructor
Initializes a wildcard with the given search pattern and options.

## Validation.ValidateObjectAttribute
http://technofattie.blogspot.de/2011/10/recursive-validation-using.html http://stackoverflow.com/questions/2690291/how-to-inherit-from-dataannotations-validationattribute-it-appears-securecritic

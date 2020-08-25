## 4.0.0 - 2020-08-25

- migrated to .Net Standard
- Plainion.Xaml namespace moved to Plainion.Windows project

## 3.5.0 - 2018-01-07

- RealFS: File.Stream() changed to always create a new file if open with "write"
- FS: "Size" property added

## 3.4.0 - 2017-07-09

- RealFS: File.Stream() changed from FileMode.Open to FileMode.OpenOrCreate to create new files on demand
- NUnit depdency updated

## 3.3.0 - 2017-07-09

- InMemoryFS: File.Stream() creates not existing files on demand

## 3.2.0 - 2017-05-07

- FileSystem.UnifyPath fixed (handling unix root)

## 3.1.0 - 2017-05-02

- IFile.Stream API added

## 3.0.0 - 2016-08-13

- Major improvement of documentation
- UnitTests added
- IO Filesystem abstraction reworked
  - FileSystem.UnifyPath: filesystem parameter removed
  - IFile.Move renamed to IFile.MoveTo
  - FileSystem.CopyTo moved to IFile.CopyTo
  - IDirectory.GetFiles() APIs renamed to EnumerateFiles and return an iterator now. 
    Convenience methods provided as extension methods so that implementations can be shared across filesystem implementations.
  - IFile.ReadAllLines and WriteAll converted into extension methods so that implementations can be shared across filesystem implementations.
- Logging framework reworked to make it easier to extend
  - BREAKING change: the application now always has to provide a logging sink
  - LoggerFactory.LoadConfiguration removed
  - ILoggerFactoringImpl renamed to ILoggerFactory
  - Most extensible classes renamed and simplified
- Processes utility moved from "IO" namespace to "Diagnostics" namespace
  
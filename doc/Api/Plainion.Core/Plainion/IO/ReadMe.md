
# Plainion.IO

This namespace extends System.IO.

## Filesystem abstraction

The filesystem abstraction provides interfaces for files, directories and related operations to decouple apps from the actual implementation of a filesystem.

This approach allows easy mocking of filesystem operations during testing as well as later replacement of the filesystem implementation for an app by e.g. Zip archive
instead of a folder structure.

### Usage

Apps which want to benefit from the filesystem abstraction need to depend on the provided interfaces only: IFileSystem, IFile, IDirectory.

In order to instantiate a filesystem abstraction implementation choose the "FileSystemImpl" from the appropriate namespace, create an instance and 
provide it as dependency to your app services (e.g. using MEF).

#### Sample

```C#
var fs = new Plainion.IO.MemoryFS.FileSystemImpl();

var root = fs.Directory( @"c:\temp" );

myFolderScanner.Search(root, "*.log");
```

### Extension

In order to extend the filesystem abstraction just provide your own impelementation of IFileSystem and related interfaces.

## Types

* [FileSystemOperations](FileSystemOperations.md)
* [IFileSystemEntry](IFileSystemEntry.md)
* [FileSystem](FileSystem.md)
* [IDirectory](IDirectory.md)
* [IFile](IFile.md)
* [IFileSystem](IFileSystem.md)

## Namespaces

* [Plainion.IO.MemoryFS](MemoryFS/ReadMe.md)
* [Plainion.IO.RealFS](RealFS/ReadMe.md)

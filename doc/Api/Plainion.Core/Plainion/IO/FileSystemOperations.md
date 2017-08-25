
# Plainion.IO.FileSystemOperations

**Namespace:** Plainion.IO

**Assembly:** Plainion.Core

Provides extensions to
*See:* T:Plainion.IO.IFile
,
*See:* T:Plainion.IO.IDirectory
and
*See:* T:Plainion.IO.IFileSystem
which implement operations which can be applied to all impelemenations of IFileSystem.


## Methods

### void WriteAll(Plainion.IO.IFile self,System.String[] text)

### System.Collections.Generic.IList`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] ReadAllLines(Plainion.IO.IFile self)

### System.Collections.Generic.IEnumerable`1[[Plainion.IO.IFile, Plainion.Core, Version=3.4.0.0, Culture=neutral, PublicKeyToken=11fdbc7b87b9a0de]] EnumerateFiles(Plainion.IO.IDirectory self)

Returns an iterator to all files in this directory.

### System.Collections.Generic.IEnumerable`1[[Plainion.IO.IFile, Plainion.Core, Version=3.4.0.0, Culture=neutral, PublicKeyToken=11fdbc7b87b9a0de]] EnumerateFiles(Plainion.IO.IDirectory self,System.String pattern)

Returns an iterator to all files in this directory matching the given wildcard pattern.

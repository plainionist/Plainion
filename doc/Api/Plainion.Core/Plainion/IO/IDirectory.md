
# Plainion.IO.IDirectory

**Namespace:** Plainion.IO

**Assembly:** Plainion.Core

Abstraction interface for a directory


## Methods

### Plainion.IO.IFile File(System.String filename)

### Plainion.IO.IDirectory Directory(System.String directory)

### System.Collections.Generic.IEnumerable`1[[Plainion.IO.IFile, Plainion.Core, Version=3.3.0.0, Culture=neutral, PublicKeyToken=11fdbc7b87b9a0de]] EnumerateFiles(System.String pattern,System.IO.SearchOption option)

Returns an iterator to all files in that directory matching the given wildcard pattern and optionally searches recursively.

### void Delete(System.Boolean recursive)

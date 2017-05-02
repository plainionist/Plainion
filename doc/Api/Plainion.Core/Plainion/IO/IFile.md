
# Plainion.IO.IFile

**Namespace:** Plainion.IO

**Assembly:** Plainion.Core

Abstraction interface for a file


## Methods

### System.IO.TextWriter CreateWriter()

### System.IO.TextWriter CreateWriter(System.Text.Encoding encoding)

### System.IO.TextReader CreateReader()

### System.IO.TextReader CreateReader(System.Text.Encoding encoding)

### System.IO.Stream Stream(System.IO.FileAccess access)

### Plainion.IO.IFile MoveTo(Plainion.IO.IDirectory directory)

Moves this file to the given target directory. Returns an instance pointing to the new target file.

### Plainion.IO.IFile CopyTo(Plainion.IO.IFileSystemEntry dirOrFile,System.Boolean overwrite)

Copies the given file to the given directory or target file. Returns an instance pointing to the new target file.

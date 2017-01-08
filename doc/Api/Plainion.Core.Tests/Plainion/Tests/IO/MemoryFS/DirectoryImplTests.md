
# Plainion.Tests.IO.MemoryFS.DirectoryImplTests

**Namespace:** Plainion.Tests.IO.MemoryFS

**Assembly:** Plainion.Core.Tests


## Constructors

### Constructor()


## Methods

### void SetUp()

### void Ctor_WhenCalled_ExistsReturnsFalse()

### void Ctor_WithDriveRoot_ExistsReturnsTrue()

### void Create_WhenCalled_ExistsReturnsTrue()

### void Create_WhenCalled_ParentDirectoriesWillBeCreatedOnDemand()

### void GetFiles_WithEmptyDirectory_ReturnsEmptyList()

### void GetFiles_DirectoryContainsFilesAndDirectories_ReturnsCreatedFilesOnly()

### void GetFiles_WithPatternAndNonEmptyDirectory_ReturnsMatchingFilesOnly()

### void GetFiles_Recursively_ReturnsAllFiles()

### void Delete_WithEmptyDirectory_ExistsShouldReturnFalse()

### void Delete_WithNonEmptyDirectory_ShouldThrow()

### void Delete_DirectoryWithContentAndRecursiveTrue_ShouldDeleteDirectoryWithContent()

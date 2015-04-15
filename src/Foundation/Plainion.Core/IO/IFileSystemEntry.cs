using System;

namespace Plainion.IO
{
    /// <summary>
    /// Abstraction interface for common functionallity of a file and a directory.
    /// </summary>
    public interface IFileSystemEntry : IComparable, IEquatable<IFileSystemEntry>
    {
        IDirectory Parent { get; }

        string Name { get; }
        string Path { get; }

        bool Exists { get; }

        void Create();
        void Delete();

        DateTime LastWriteTime { get; }
        DateTime LastAccessTime { get; }
    }
}

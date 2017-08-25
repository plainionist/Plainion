using System;
using System.IO;
using System.Text;

namespace Plainion.IO.RealFS
{
    internal class FileImpl : AbstractFileSystemEntry<FileSystemImpl>, IFile
    {
        public FileImpl(FileSystemImpl fileSystem, string path)
            : base(fileSystem, path)
        {
        }

        public override bool Exists
        {
            get { return File.Exists(Path); }
        }

        public override void Create()
        {
            File.Create(Path);
        }

        public override void Delete()
        {
            File.Delete(Path);
        }

        public TextWriter CreateWriter()
        {
            return new StreamWriter(Path);
        }

        public TextWriter CreateWriter(Encoding encoding)
        {
            return new StreamWriter(Path, false, encoding);
        }

        public TextReader CreateReader()
        {
            return new StreamReader(Path);
        }

        public TextReader CreateReader(Encoding encoding)
        {
            return new StreamReader(Path, encoding);
        }

        public Stream Stream(FileAccess access)
        {
            return new FileStream(Path, FileMode.OpenOrCreate, access);
        }

        public override DateTime LastWriteTime
        {
            get { return File.GetLastWriteTime(Path); }
        }

        public override DateTime LastAccessTime
        {
            get { return File.GetLastAccessTime(Path); }
        }

        public IFile MoveTo(IDirectory directory)
        {
            if(!directory.Exists)
            {
                directory.Create();
            }

            var targetFile = directory.File(Name);
            File.Move(Path, targetFile.Path);

            return targetFile;
        }

        public IFile CopyTo(IFileSystemEntry dirOrFile, bool overwrite)
        {
            var target = dirOrFile.Path;

            if(dirOrFile is IDirectory)
            {
                target = System.IO.Path.Combine(dirOrFile.Path, Name);
            }

            File.Copy(Path, target, overwrite);

            return FileSystem.File(target);
        }
    }
}

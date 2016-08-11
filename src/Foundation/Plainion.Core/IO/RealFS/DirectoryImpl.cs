using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NDirectory = System.IO.Directory;
using NPath = System.IO.Path;

namespace Plainion.IO.RealFS
{
    internal class DirectoryImpl : AbstractFileSystemEntry<FileSystemImpl>, IDirectory
    {
        public DirectoryImpl( FileSystemImpl fileSystem, string path )
            : base( fileSystem,path )
        {
        }
        
        public override bool Exists
        {
            get { return NDirectory.Exists( Path ); }
        }

        public override void Create()
        {
            if ( NDirectory.Exists( Path ) )
            {
                return;
            }

            NDirectory.CreateDirectory( Path );
        }

        public override void Delete()
        {
            NDirectory.Delete( Path );
        }

        public IEnumerable<IFile> EnumerateFiles()
        {
            return NDirectory.EnumerateFiles( Path )
                .Select( path => FileSystem.File( path ) );
        }

        public IEnumerable<IFile> EnumerateFiles( string pattern )
        {
            return NDirectory.EnumerateFiles( Path, pattern )
                .Select( path => FileSystem.File( path ) );
        }

        public IEnumerable<IFile> EnumerateFiles( string pattern, SearchOption option )
        {
            return NDirectory.EnumerateFiles( Path, pattern, option )
                .Select( path => FileSystem.File( path ) );
        }

        public void Delete( bool recursive )
        {
            NDirectory.Delete( Path, recursive );
        }

        public IFile File( string filename )
        {
            return FileSystem.File( NPath.Combine( Path, filename ) );
        }

        public IDirectory Directory( string directory )
        {
            return FileSystem.Directory( NPath.Combine( Path, directory ) );
        }

        public override DateTime LastWriteTime
        {
            get { return NDirectory.GetLastWriteTime( Path ); }
        }

        public override DateTime LastAccessTime
        {
            get { return NDirectory.GetLastAccessTime( Path ); }
        }
    }
}

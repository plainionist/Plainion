using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Plainion.Text;
using NPath = System.IO.Path;

namespace Plainion.IO.MemoryFS
{
    [DataContract( Namespace = DCNames.NS_MemoryFS)]
    internal class DirectoryImpl : AbstractFileSystemEntry<FileSystemImpl>, IDirectory
    {
        [DataMember]
        private bool myExists;

        public DirectoryImpl( FileSystemImpl fileSystem, string path )
            : base( fileSystem, path )
        {
            // drive root always exists 
            myExists = Path.Length <= 2 ? true : false;
        }

        public override bool Exists
        {
            get { return myExists; }
        }

        public override void Create()
        {
            if ( Exists )
            {
                return;
            }

            if ( Parent != null )
            {
                Parent.Create();
            }

            myExists = true;
        }

        public IEnumerable<IFile> EnumerateFiles()
        {
            return EnumerateFiles( null );
        }

        public IEnumerable<IFile> EnumerateFiles( string pattern )
        {
            return EnumerateFiles( pattern, SearchOption.TopDirectoryOnly );
        }

        public IEnumerable<IFile> EnumerateFiles( string pattern, SearchOption option )
        {
            if ( !Exists )
            {
                return new IFile[] { };
            }

            var allChildren = GetAllExistingChildren( option == SearchOption.AllDirectories );

            var filesToReturn = allChildren.OfType<IFile>();

            if ( !string.IsNullOrEmpty( pattern ) )
            {
                filesToReturn = filesToReturn.Where( file => Wildcard.IsMatch( file.Name, pattern ) );
            }

            return filesToReturn;
        }

        private IEnumerable<IFileSystemEntry> GetAllExistingChildren( bool recursive )
        {
            var allFiles = FileSystem.Items
                .Where( file => file.Exists )
                .ToList(); // "Directory" property below will create new items

            if ( recursive )
            {
                return allFiles.Where( item => ItemIsChild( item ) );
            }
            else
            {
                return allFiles.Where( file => Equals( file.Parent ) );
            }
        }

        // checks if item belongs to this directory recursively
        private bool ItemIsChild( IFileSystemEntry item )
        {
            if ( !item.Path.StartsWith( Path, StringComparison.OrdinalIgnoreCase ) )
            {
                return false;
            }

            var itemParent = item.Parent;
            while ( itemParent != null )
            {
                if ( Equals( itemParent ) )
                {
                    return true;
                }

                itemParent = itemParent.Parent;
            }

            return false;
        }

        public override void Delete()
        {
            if ( GetAllExistingChildren( false ).Any() )
            {
                throw new IOException( "Directory is not empty" );
            }

            myExists = false;
        }

        public void Delete( bool recursive )
        {
            if ( !recursive )
            {
                Delete();
                return;
            }

            var allChildren = GetAllExistingChildren( recursive ).ToList();
            foreach ( var item in allChildren )
            {
                var dir = item as IDirectory;
                if ( dir != null )
                {
                    dir.Delete( true );
                }
                else
                {
                    item.Delete();
                }
            }

            myExists = false;
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
            // not supported yet
            get { return DateTime.MinValue; }
        }

        public override DateTime LastAccessTime
        {
            // not supported yet
            get { return DateTime.MinValue; }
        }
    }
}

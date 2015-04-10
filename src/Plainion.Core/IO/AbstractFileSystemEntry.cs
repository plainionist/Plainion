using System;
using System.Runtime.Serialization;
using NPath = System.IO.Path;

namespace Plainion.IO
{
    [DataContract( Namespace = DCNames.NS_AbstractFS)]
    internal abstract class AbstractFileSystemEntry<TFileSystem> : IFileSystemEntry where TFileSystem : IFileSystem
    {
        protected AbstractFileSystemEntry( TFileSystem fileSystem, string path )
        {
            if ( fileSystem == null )
            {
                throw new ArgumentNullException( "fileSystem" );
            }
            if ( string.IsNullOrWhiteSpace( path ) )
            {
                throw new ArgumentNullException( "path" );
            }

            FileSystem = fileSystem;
            Path = FileSystem.UnifyPath( path );
        }

        [DataMember]
        protected TFileSystem FileSystem
        {
            get;
            private set;
        }

        [DataMember]
        public string Path
        {
            get;
            private set;
        }

        public string Name
        {
            get { return NPath.GetFileName( Path ); }
        }

        public IDirectory Parent
        {
            get
            {
                var parent = NPath.GetDirectoryName( Path );

                return parent == null ? null : FileSystem.Directory( parent );
            }
        }

        public abstract bool Exists { get; }

        public abstract void Create();

        public abstract void Delete();

        public abstract DateTime LastWriteTime { get; }
        public abstract DateTime LastAccessTime { get; }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return -1;
            }

            var fsItem = obj as IFileSystemEntry;
            if ( fsItem == null )
            {
                throw new ArgumentException( string.Format( "Cannot compare {0} and {1}", GetType(), obj.GetType() ) );
            }

            return Path.CompareTo( fsItem.Path );
        }

        public bool Equals( IFileSystemEntry other )
        {
            return Equals( (object)other );
        }

        public override bool Equals( object obj )
        {
            if ( object.ReferenceEquals( this, obj ) )
            {
                return true;
            }

            var otherFsItem = obj as IFileSystemEntry;
            if ( object.ReferenceEquals( otherFsItem, null ) )
            {
                return false;
            }

            return Path.Equals( otherFsItem.Path );
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }

        public override string ToString()
        {
            return Path;
        }
    }
}

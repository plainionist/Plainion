using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Plainion.IO.MemoryFS
{
    [DataContract( Namespace = DCNames.NS_MemoryFS )]
    [KnownType( typeof( DirectoryImpl ) )]
    [KnownType( typeof( FileImpl ) )]
    public class FileSystemImpl : IFileSystem
    {
        [DataMember( Name = "Items" )]
        private Dictionary<string, IFileSystemEntry> myItems;

        public FileSystemImpl()
        {
            myItems = new Dictionary<string, IFileSystemEntry>( StringComparer.OrdinalIgnoreCase );
        }

        public IDirectory Directory( string path )
        {
            var absolutePath = FileSystem.UnifyPath( path );

            if( !myItems.ContainsKey( absolutePath ) )
            {
                AddItem( new DirectoryImpl( this, absolutePath ) );
            }

            return ( IDirectory )myItems[ absolutePath ];
        }

        public IFile File( string path )
        {
            var absolutePath = FileSystem.UnifyPath( path );

            if( !myItems.ContainsKey( absolutePath ) )
            {
                AddItem( new FileImpl( this, absolutePath ) );
            }

            return ( IFile )myItems[ absolutePath ];
        }

        public IFile GetTempFile()
        {
            var file = new FileImpl( this, Path.GetTempFileName() );
            file.Parent.Create();

            return file;
        }

        public IDirectory GetTempPath()
        {
            var dir = new DirectoryImpl( this, Path.GetTempPath() );
            dir.Create();

            return dir;
        }

        internal IEnumerable<IFileSystemEntry> Items
        {
            get { return myItems.Values; }
        }

        internal void AddItem( IFileSystemEntry item )
        {
            myItems[ item.Path ] = item;
        }

        internal void RemoveItem( IFileSystemEntry item )
        {
            if( myItems.ContainsKey( item.Path ) )
            {
                myItems.Remove( item.Path );
            }
        }

        public void Serialize( Stream stream )
        {
            var serializer = CreateSerializer();
            serializer.WriteObject( stream, this );
        }

        private static DataContractSerializer CreateSerializer()
        {
            var settings = new DataContractSerializerSettings();
            settings.IgnoreExtensionDataObject = true;
            settings.PreserveObjectReferences = true;
            return new DataContractSerializer( typeof( FileSystemImpl ), settings );
        }

        public static FileSystemImpl Deserialize( Stream stream )
        {
            var serializer = new DataContractSerializer( typeof( FileSystemImpl ) );
            return ( FileSystemImpl )serializer.ReadObject( stream );
        }
    }
}

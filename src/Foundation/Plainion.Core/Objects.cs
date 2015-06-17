using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Plainion
{
    public static class Objects
    {
        /// <summary>
        /// Generic object tree deep clone using BinaryFormatter.
        /// </summary>
        public static T Clone<T>( T source )
        {
            Contract.RequiresNotNull( source, "source" );
            Contract.Requires( source.GetType().IsSerializable, "Input type '{0}' must be serializable", typeof( T ) );

            var formatter = new BinaryFormatter();
            using( var stream = new MemoryStream() )
            {
                formatter.Serialize( stream, source );
                stream.Seek( 0, SeekOrigin.Begin );
                return ( T )formatter.Deserialize( stream );
            }
        }
    }
}

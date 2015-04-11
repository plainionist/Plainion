using System.IO;
using System.Windows.Markup;
using System.Xml.Linq;
using Plainion.Validation;

namespace Plainion.Xaml
{
    /// <summary>
    /// Reads given Xaml input and validates it using DataAnnotations and <see cref="RecursiveValidator"/>
    /// </summary>
    public class ValidatingXamlReader 
    {
        public T Read<T>( string path )
        {
            using ( var stream = new FileStream( path, FileMode.Open ) )
            {
                return Read<T>( stream );
            }
        }

        public T Read<T>( Stream stream )
        {
            var obj = XamlReader.Load( stream );

            RecursiveValidator.Validate( obj );

            return (T)obj;
        }

        public T Read<T>( XElement xml )
        {
            using ( var reader = xml.CreateReader() )
            {
                var obj = XamlReader.Load( reader );

                RecursiveValidator.Validate( obj );

                return (T)obj;
            }
        }
    }
}

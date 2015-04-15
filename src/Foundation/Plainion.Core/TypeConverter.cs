using System;
using System.Globalization;

namespace Plainion
{
    public class TypeConverter
    {
        public static object ConvertTo( string value, Type type )
        {
            if ( type == typeof( double ) )
            {
                return Convert.ToDouble( value, CultureInfo.InvariantCulture );
            }
            if ( type == typeof( double? ) )
            {
                return new Nullable<double>( Convert.ToDouble( value, CultureInfo.InvariantCulture ) );
            }
            if ( type == typeof( int ) )
            {
                return Convert.ToInt32( value, CultureInfo.InvariantCulture );
            }
            if ( type == typeof( int? ) )
            {
                return new Nullable<int>( Convert.ToInt32( value, CultureInfo.InvariantCulture ) );
            }
            if ( type == typeof( long ) )
            {
                return Convert.ToInt64( value, CultureInfo.InvariantCulture );
            }
            if ( type == typeof( long? ) )
            {
                return new Nullable<long>( Convert.ToInt64( value, CultureInfo.InvariantCulture ) );
            }
            if ( type == typeof( string ) )
            {
                return value.ToString();
            }
            else if ( type == typeof( bool ) )
            {
                return Convert.ToBoolean( value, CultureInfo.InvariantCulture );
            }
            else if ( type == typeof( bool? ) )
            {
                return new Nullable<bool>( Convert.ToBoolean( value, CultureInfo.InvariantCulture ) );
            }
            else if ( type == typeof( DateTime ) )
            {
                return Convert.ToDateTime( value, CultureInfo.InvariantCulture );
            }
            else if ( type == typeof( DateTime? ) )
            {
                return new Nullable<DateTime>( Convert.ToDateTime( value, CultureInfo.InvariantCulture ) );
            }
            else
            {
                throw new NotSupportedException( "Convertion from string into type:" + type );
            }
        }
    }
}

using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Plainion.Windows
{
    public class IsEmptyConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if( value == null )
            {
                return true;
            }

            var collection = value as IEnumerable;
            if( collection != null )
            {
                return !collection.GetEnumerator().MoveNext();
            }

            var str = value as string;
            if( str != null )
            {
                return string.IsNullOrEmpty( str );
            }

            throw new NotSupportedException( value.GetType().ToString() );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}

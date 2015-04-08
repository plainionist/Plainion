using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Plainion.Windows
{
    public class OrConverter : List<IValueConverter>,  IMultiValueConverter
    {
        public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
        {
            bool ret = false;

            foreach( var value in values )
            {
                if( value is bool )
                {
                    ret = ret || ( bool )value;
                }
            }

            return this.Aggregate( (object)ret, ( current, converter ) => converter.Convert( current, targetType, parameter, culture ) );
        }

        public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}

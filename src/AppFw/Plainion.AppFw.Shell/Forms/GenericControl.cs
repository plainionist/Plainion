using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Plainion;

namespace Plainion.AppFw.Shell.Forms
{
    public class GenericControl : ControlBase
    {
        public GenericControl( object owner, PropertyInfo property, ArgumentAttribute attribute )
            : base( owner, property, attribute )
        {
        }

        protected override void Bind( string argument, Queue<string> additionalArguments )
        {
            if ( Property.PropertyType == typeof( bool ) )
            {
                Property.SetValue( Owner, true, null );
            }
            else if ( Property.GetValue( Owner, null ) is IDictionary )
            {
                var valueArg = GetValueArgument( argument, additionalArguments );

                var dictionary = (IDictionary)Property.GetValue( Owner, null );

                int pos = valueArg.IndexOf( '=' );
                dictionary[ valueArg.Substring( 0, pos ) ] = valueArg.Substring( pos + 1 );
            }
            else if ( Property.GetValue( Owner, null ) is IList )
            {
                var valueArg = GetValueArgument( argument, additionalArguments );

                var collection = (IList)Property.GetValue( Owner, null );

                collection.Add( valueArg );
            }
            else
            {
                var valueArg = GetValueArgument( argument, additionalArguments );

                Property.SetValue( Owner, TypeConverter.ConvertTo( valueArg, Property.PropertyType ), null );
            }
        }
    }
}

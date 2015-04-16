using System;
using System.Linq;
using System.Reflection;

namespace Plainion.AppFw.Shell.Forms
{
    public class ControlFactory
    {
        public static IControl TryCreate( object owner, PropertyInfo property )
        {
            var attr = property.GetCustomAttributes( typeof( ArgumentAttribute ), true ).SingleOrDefault();

            if ( attr is ConfigFileArgumentAttribute )
            {
                return new ConfigFileControl( owner, property, (ArgumentAttribute)attr );
            }

            if ( attr != null )
            {
                return new GenericControl( owner, property, (ArgumentAttribute)attr );
            }

            attr = property.GetCustomAttributes( typeof( UserControlAttribute ), true ).SingleOrDefault();

            if ( attr != null )
            {
                var instance = property.GetValue( owner, null );
                if ( instance == null )
                {
                    instance = Activator.CreateInstance( property.PropertyType );
                    property.SetValue( owner, instance, null );
                }

                return new UserControl( instance );
            }

            return null;
        }
    }
}

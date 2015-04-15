using System.Collections.Generic;
using System.Reflection;
using Plainion.Xaml;

namespace Plainion.AppFw.Shell.Forms
{
    public class ConfigFileControl : ControlBase
    {
        public ConfigFileControl( object owner, PropertyInfo property, ArgumentAttribute attribute )
            : base( owner, property, attribute )
        {
        }

        protected override void Bind( string argument, Queue<string> additionalArguments )
        {
            var valueArg = GetValueArgument( argument, additionalArguments );

            var config = new ValidatingXamlReader().Read<object>( valueArg );

            Property.SetValue( Owner, config, null );
        }
    }
}

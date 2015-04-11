using System;
using System.Windows.Markup;

namespace Plainion.Xaml
{
    /// <summary>
    /// Xaml markup comparable to XInclude. Allows simple include of Xaml files into other Xaml files.
    /// </summary>
    [MarkupExtensionReturnType( typeof( object ) )]
    public class IncludeExtension : MarkupExtension 
    {
        public string Path
        {
            get;
            set;
        }

        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            var reader = new ValidatingXamlReader();
            return reader.Read<object>( Path );
        }
    }
}

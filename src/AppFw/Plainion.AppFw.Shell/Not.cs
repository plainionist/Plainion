using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Markup;

namespace Plainion.AppFw.Shell
{
    [MarkupExtensionReturnType( typeof( Func<bool> ) )]
    public class Not : MarkupExtension
    {
        [Required]
        public Func<bool> Condition
        {
            get;
            set;
        }

        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            Func<bool> func = () => !Condition();
            return func;
        }
    }
}

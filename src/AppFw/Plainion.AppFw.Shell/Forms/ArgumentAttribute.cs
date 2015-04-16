using System;
using System.ComponentModel.DataAnnotations;
using Plainion;

namespace Plainion.AppFw.Shell.Forms
{
    public class ArgumentAttribute : Attribute
    {
        [Required]
        public string Short
        {
            get;
            set;
        }

        public string Long
        {
            get;
            set;
        }

        [Required]
        public string Description
        {
            get;
            set;
        }

        public bool IsMatch( string arg )
        {
            return Short.Equals( arg, StringComparison.OrdinalIgnoreCase ) || (Long != null && Long.Equals( arg, StringComparison.OrdinalIgnoreCase ));
        }
    }
}

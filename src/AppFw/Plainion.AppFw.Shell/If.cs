using System;
using System.ComponentModel.DataAnnotations;

namespace Plainion.AppFw.Shell
{
    public class If : Sequence
    {
        [Required]
        public Func<bool> Condition
        {
            get;
            set;
        }

        protected override void ExecuteInternal( string[] args )
        {
            if ( Condition() )
            {
                base.ExecuteInternal( args );
            }
            else
            {
                Console.WriteLine( " ... Skipped" );
            }
        }
    }
}

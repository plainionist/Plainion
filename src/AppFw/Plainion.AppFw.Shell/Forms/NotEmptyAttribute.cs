using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plainion.AppFw.Shell.Forms
{
    public class NotEmptyAttribute : ValidationAttribute
    {
        public NotEmptyAttribute()
            : base( "{0} is not a collection or is empty" )
        {
        }

        public override bool IsValid( object value )
        {
            var collection = value as IEnumerable;
            if ( collection == null )
            {
                return false;
            }

            return collection.OfType<object>().Any();
        }
    }
}

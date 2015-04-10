using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Plainion.Validation
{
    public static class RecursiveValidator
    {
        public static void Validate( object root )
        {
            var ctx = new ValidationContext( root, null, null );

            var results = new List<ValidationResult>();
            if ( !Validator.TryValidateObject( root, ctx, results, true ) )
            {
                throw new ValidationException( GetCombinedMessage( results ) );
            }
        }

        private static string GetCombinedMessage( IEnumerable<ValidationResult> results )
        {
            var sb = new StringBuilder();

            sb.Append( "Validation failed: " );

            foreach ( var result in ExpandResults( results ) )
            {
                sb.Append( Environment.NewLine );
                sb.Append( result.ErrorMessage );
            }

            return sb.ToString();
        }

        public static IEnumerable<ValidationResult> ExpandResults( ValidationResult result )
        {
            if ( result == null )
            {
                return new List<ValidationResult>();
            }

            return ExpandResults( new List<ValidationResult> { result } );
        }

        public static IEnumerable<ValidationResult> ExpandResults( IEnumerable<ValidationResult> results )
        {
            foreach ( var result in results )
            {
                var composite = result as CompositeValidationResult;
                if ( composite != null )
                {
                    foreach ( var nestedResult in ExpandResults( composite.Results ) )
                    {
                        yield return nestedResult;
                    }
                }
                else
                {
                    yield return result;
                }
            }
        }
    }
}

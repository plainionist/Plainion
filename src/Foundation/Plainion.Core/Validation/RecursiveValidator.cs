using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;

namespace Plainion.Validation
{
    public static class RecursiveValidator
    {
        public static void Validate( object root )
        {
            var ctx = new ValidationContext( root, null, null );

            var results = new List<ValidationResult>();
            if( !Validator.TryValidateObject( root, ctx, results, true ) )
            {
                throw new ValidationException( GetCombinedMessage( results ) );
            }
        }

        private static string GetCombinedMessage( IEnumerable<ValidationResult> results )
        {
            var sb = new StringBuilder();

            sb.Append( "Validation failed: " );

            foreach( var result in ExpandResults( results ) )
            {
                sb.Append( Environment.NewLine );
                sb.Append( result );
            }

            return sb.ToString();
        }

        public static IEnumerable<string> ExpandResults( ValidationResult result )
        {
            if( result == null )
            {
                return new List<string>();
            }

            return ExpandResults( new List<ValidationResult> { result } );
        }

        public static IEnumerable<string> ExpandResults( IEnumerable<ValidationResult> results )
        {
            return ExpandResultsInternal( results )
                .Select( i => i.Item2 );
        }

        private static IEnumerable<Tuple<bool, string>> ExpandResultsInternal( IEnumerable<ValidationResult> results )
        {
            foreach( var result in results )
            {
                var composite = result as CompositeValidationResult;
                if( composite == null )
                {
                    yield return Tuple.Create( false, result.ErrorMessage );
                }
                else
                {
                    foreach( var nestedResult in ExpandResultsInternal( composite.Results ) )
                    {
                        yield return Tuple.Create( true, composite.Context + ( nestedResult.Item1 ? "." : ": " ) + nestedResult.Item2 );
                    }
                }
            }
        }
    }
}

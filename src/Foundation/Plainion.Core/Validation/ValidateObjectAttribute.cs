using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plainion.Validation
{
    /// <summary>
    /// http://technofattie.blogspot.de/2011/10/recursive-validation-using.html
    /// http://stackoverflow.com/questions/2690291/how-to-inherit-from-dataannotations-validationattribute-it-appears-securecritic
    /// </summary>
    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid( object value, ValidationContext validationContext )
        {
            if( value == null )
            {
                // we cannot validate the properties of the given object when it is null
                return ValidationResult.Success;
            }

            var dictionary = value as IDictionary;
            if( dictionary != null )
            {
                return ValidateDictionary( dictionary, validationContext );
            }

            var enumerable = value as IEnumerable;
            if( enumerable != null )
            {
                return ValidateEnumerable( enumerable.OfType<object>(), validationContext );
            }

            return ValidateObject( value, validationContext );
        }

        private ValidationResult ValidateDictionary( IDictionary dictionary, ValidationContext validationContext )
        {
            var compositeResults = new CompositeValidationResult( validationContext.DisplayName, string.Format( "Validation for {0} failed!", validationContext.DisplayName ) );

            var resultForKeys = ValidateEnumerable( dictionary.Keys.OfType<object>(), validationContext );
            if( resultForKeys != ValidationResult.Success )
            {
                compositeResults.AddResult( resultForKeys );
            }

            var resultForValues = ValidateEnumerable( dictionary.Values.OfType<object>(), validationContext );
            if( resultForValues != ValidationResult.Success )
            {
                compositeResults.AddResult( resultForValues );
            }


            return compositeResults.Results.Any() ? compositeResults : ValidationResult.Success;
        }

        private ValidationResult ValidateEnumerable( IEnumerable<object> enumerable, ValidationContext validationContext )
        {
            var results = enumerable
                .Select( item => IsValid( item, new ValidationContext( item, null, null ) ) )
                .Where( result => result != ValidationResult.Success )
                .ToList();

            if( !results.Any() )
            {
                return ValidationResult.Success;
            }

            var compositeResults = new CompositeValidationResult( validationContext.DisplayName, string.Format( "Validation for {0} failed!", validationContext.DisplayName ) );
            results.ForEach( compositeResults.AddResult );

            return compositeResults;
        }

        private ValidationResult ValidateObject( object value, ValidationContext validationContext )
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext( value, null, null );

            Validator.TryValidateObject( value, context, results, true );
            if( results.Count == 0 )
            {
                return ValidationResult.Success;
            }

            var compositeResults = new CompositeValidationResult( validationContext.DisplayName, string.Format( "Validation for {0} failed!", validationContext.DisplayName ) );
            results.ForEach( compositeResults.AddResult );

            return compositeResults;
        }
    }
}

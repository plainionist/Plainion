using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plainion.Validation
{
    /// <summary>
    /// Composite pattern for System.ComponentModel.DataAnnotation.ValidationResult
    /// </summary>
    public class CompositeValidationResult : ValidationResult
    {
        private readonly List<ValidationResult> myResults = new List<ValidationResult>();

        public CompositeValidationResult( string context, string errorMessage )
            : base( errorMessage )
        {
            Context = context;
        }

        public CompositeValidationResult( string validatedType, string errorMessage, IEnumerable<string> memberNames )
            : base( errorMessage, memberNames )
        {
            Context = validatedType;
        }

        public string Context { get; private set; }

        public IEnumerable<ValidationResult> Results { get { return myResults; } }

        public void AddResult( ValidationResult validationResult )
        {
            myResults.Add( validationResult );
        }
    }
}

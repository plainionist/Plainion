using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plainion.Validation
{
    public class CompositeValidationResult : ValidationResult
    {
        private readonly List<ValidationResult> myResults = new List<ValidationResult>();

        public CompositeValidationResult( string errorMessage )
            : base( errorMessage )
        {
        }

        public CompositeValidationResult( string errorMessage, IEnumerable<string> memberNames )
            : base( errorMessage, memberNames )
        {
        }

        protected CompositeValidationResult( ValidationResult validationResult )
            : base( validationResult )
        {
        }

        public IEnumerable<ValidationResult> Results
        {
            get
            {
                return myResults;
            }
        }

        public void AddResult( ValidationResult validationResult )
        {
            myResults.Add( validationResult );
        }
    }
}

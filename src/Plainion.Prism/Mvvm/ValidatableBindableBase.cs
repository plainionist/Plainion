using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.ViewModel;

namespace Plainion.Controls.Mvvm
{
    // more advanced: http://blog.pluralsight.com/async-validation-wpf-prism
    public class ValidatableBindableBase : BindableBase, INotifyDataErrorInfo
    {
        private ErrorsContainer<ValidationFailure> myErrorsContainer;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors( string propertyName )
        {
            return ErrorsContainer.GetErrors( propertyName );
        }

        public bool HasErrors
        {
            get { return ErrorsContainer.HasErrors; }
        }

        protected ErrorsContainer<ValidationFailure> ErrorsContainer
        {
            get
            {
                if( myErrorsContainer == null )
                {
                    myErrorsContainer =
                        new ErrorsContainer<ValidationFailure>( pn => RaiseErrorsChanged( pn ) );
                }

                return myErrorsContainer;
            }
        }

        private void RaiseErrorsChanged( string propertyName )
        {
            OnErrorsChanged( new DataErrorsChangedEventArgs( propertyName ) );
        }

        protected virtual void OnErrorsChanged( DataErrorsChangedEventArgs e )
        {
            if( ErrorsChanged != null )
            {
                ErrorsChanged( this, e );
            }
        }

        public void SetErrors( IEnumerable<ValidationFailure> errors, [CallerMemberName] string propertyName = null )
        {
            ErrorsContainer.SetErrors( propertyName, errors );
        }

        public void SetError( ValidationFailure error, [CallerMemberName] string propertyName = null )
        {
            ErrorsContainer.SetErrors( propertyName, new[] { error } );
        }

        public void ClearErrors( [CallerMemberName] string propertyName = null )
        {
            ErrorsContainer.ClearErrors( propertyName );
        }
    }
}

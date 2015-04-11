using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Plainion.Windows
{
    internal class PropertyAccessor
    {
        private PropertyInfo myProperty;

        private PropertyAccessor( INotifyPropertyChanged owner, PropertyInfo property )
        {
            Contract.RequiresNotNull( owner, "owner" );
            Contract.RequiresNotNull( property, "property" );

            Owner = owner;
            myProperty = property;
        }

        public INotifyPropertyChanged Owner { get; private set; }

        public string PropertyName { get { return myProperty.Name; } }

        public void SetValue( object value )
        {
            myProperty.SetValue( Owner, value );
        }

        public object GetValue()
        {
            return myProperty.GetValue( Owner );
        }

        public static PropertyAccessor Create( INotifyPropertyChanged owner, string propertyName )
        {
            Contract.RequiresNotNull( owner, "owner" );
            
            return new PropertyAccessor( owner, owner.GetType().GetProperty( propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic ) );
        }

        public static PropertyAccessor Create<T>( Expression<Func<T>> expr )
        {
            Contract.RequiresNotNull( expr, "expr" );

            var memberExpression = expr.Body as MemberExpression;
            Contract.Requires( memberExpression != null, "Given expression is not a member expression" );

            var propertyInfo = memberExpression.Member as PropertyInfo;
            Contract.Requires( memberExpression != null, "Given member expression is not a property" );

            Contract.Requires( !propertyInfo.GetMethod.IsStatic, "Static properties are not supported" );

            var owner = GetOwner( memberExpression );

            return new PropertyAccessor( owner, propertyInfo );
        }

        private static INotifyPropertyChanged GetOwner( MemberExpression memberExpression )
        {
            var ce = memberExpression.Expression as ConstantExpression;
            if( ce != null )
            {
                return ( INotifyPropertyChanged )ce.Value;
            }

            var pe = memberExpression.Expression as MemberExpression;
            if( pe == null )
            {
                throw new NotSupportedException( memberExpression.Expression.GetType().ToString() );
            }

            ce = ( ConstantExpression )pe.Expression;

            var fieldInfo = ce.Value.GetType().GetField( pe.Member.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
            if( fieldInfo != null )
            {
                return ( INotifyPropertyChanged )fieldInfo.GetValue( ce.Value );
            }

            var pi = ce.Value.GetType().GetProperty( pe.Member.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
            if( pi != null )
            {
                return ( INotifyPropertyChanged )pi.GetValue( ce.Value );
            }

            throw new NotSupportedException( "Failed to get owner" );
        }
    }
}

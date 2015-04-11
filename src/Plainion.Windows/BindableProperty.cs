﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Plainion.Windows
{
    /// <summary>
    /// References a property which provides change notifications
    /// </summary>
    public class BindableProperty
    {
        private PropertyInfo myProperty;

        private BindableProperty( INotifyPropertyChanged owner, PropertyInfo property )
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

        public static BindableProperty Create( INotifyPropertyChanged owner, string propertyName )
        {
            Contract.RequiresNotNull( owner, "owner" );

            return new BindableProperty( owner, owner.GetType().GetProperty( propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic ) );
        }

        public static BindableProperty Create<T>( Expression<Func<T>> expr )
        {
            Contract.RequiresNotNull( expr, "expr" );

            var memberExpr = expr.Body as MemberExpression;
            Contract.Requires( memberExpr != null, "Given expression is not a member expression" );

            var propertyInfo = memberExpr.Member as PropertyInfo;
            Contract.Requires( memberExpr != null, "Given member expression is not a property" );

            Contract.Requires( !propertyInfo.GetMethod.IsStatic, "Static properties are not supported" );

            var owner = GetOwner( memberExpr ) as INotifyPropertyChanged;
            Contract.Requires( owner != null, "Owner of property '" + propertyInfo.Name + "' does not implement INotifyPropertyChanged" );

            return new BindableProperty( owner, propertyInfo );
        }

        private static object GetOwner( MemberExpression memberExpr )
        {
            var constantExpr = memberExpr.Expression as ConstantExpression;
            if( constantExpr != null )
            {
                // 
                return constantExpr.Value;
            }

            var nestedMemberExpr = memberExpr.Expression as MemberExpression;
            Contract.Invariant( nestedMemberExpr != null, "Failed to handle nested expression type: " + memberExpr.Expression.GetType().ToString() );

            object instance = null;

            constantExpr = nestedMemberExpr.Expression as ConstantExpression;
            if( constantExpr != null )
            {
                instance = constantExpr.Value;
            }
            else
            {
                instance = GetOwner( nestedMemberExpr );
            }

            var fieldInfo = instance.GetType().GetField( nestedMemberExpr.Member.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
            if( fieldInfo != null )
            {
                return fieldInfo.GetValue( instance );
            }

            var propertyInfo = instance.GetType().GetProperty( nestedMemberExpr.Member.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
            if( propertyInfo != null )
            {
                return propertyInfo.GetValue( instance );
            }

            throw new NotSupportedException( "Failed to get owner" );
        }
    }
}

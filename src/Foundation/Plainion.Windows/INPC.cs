using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Plainion.Windows
{
    class INPC
    {
        internal static string PropertyName<T>( Expression<Func<T>> expr )
        {
            Contract.RequiresNotNull( expr, "expr" );

            var memberExpr = expr.Body as MemberExpression;
            Contract.Requires( memberExpr != null, "Given expression is not a member expression" );

            var propertyInfo = memberExpr.Member as PropertyInfo;
            Contract.Requires( propertyInfo != null, "Given member expression is not a property" );

            Contract.Requires( !propertyInfo.GetMethod.IsStatic, "Static properties are not supported" );

            return propertyInfo.Name;
        }
    }
}

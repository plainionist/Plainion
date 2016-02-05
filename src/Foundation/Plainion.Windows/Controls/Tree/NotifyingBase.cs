using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Plainion.Windows.Controls.Tree
{
    public class NotifyingBase : TreeViewItem, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            RaisePropertyChanged(propertyName);
        }

        protected string GetPropertyName<T>(Expression<Func<T>> expr)
        {
            Contract.RequiresNotNull(expr, "expr");

            var memberExpr = expr.Body as MemberExpression;
            Contract.Requires(memberExpr != null, "Given expression is not a member expression");

            var propertyInfo = memberExpr.Member as PropertyInfo;
            Contract.Requires(propertyInfo != null, "Given member expression is not a property");

            return propertyInfo.Name;
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> expr)
        {
            OnPropertyChanged(GetPropertyName(expr));
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }

            storage = value;

            RaisePropertyChanged(propertyName);

            return true;
        }
    }
}

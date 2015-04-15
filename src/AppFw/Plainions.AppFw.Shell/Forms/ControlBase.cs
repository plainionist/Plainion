using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Plainion;

namespace Plainion.AppFw.Shell.Forms
{
    public abstract class ControlBase : IControl
    {
        protected ControlBase( object owner, PropertyInfo property, ArgumentAttribute attribute )
        {
            Contract.RequiresNotNull( owner, "owner" );
            Contract.RequiresNotNull( property, "property" );
            Contract.RequiresNotNull( attribute, "attribute" );

            Owner = owner;
            Property = property;
            Attribute = attribute;
        }

        public object Owner
        {
            get;
            private set;
        }

        protected PropertyInfo Property
        {
            get;
            private set;
        }

        protected ArgumentAttribute Attribute
        {
            get;
            private set;
        }

        public bool TryBind( string argument, Queue<string> additionalArguments )
        {
            if ( !Attribute.IsMatch( argument ) )
            {
                return false;
            }

            Bind( argument, additionalArguments );

            return true;
        }

        protected abstract void Bind( string argument, Queue<string> additionalArguments );

        protected string GetValueArgument( string argument, Queue<string> additionalArguments )
        {
            if ( !additionalArguments.Any() )
            {
                throw new InvalidOperationException( string.Format( "Argument '{0}' requires a value", argument ) );
            }

            return additionalArguments.Dequeue();
        }

        public void Describe( TextWriter writer )
        {
            var options = Attribute.Short;

            if ( !string.IsNullOrEmpty( Attribute.Long ) )
            {
                options += "/" + Attribute.Long;
            }

            writer.WriteLine( "{0,-30} - {1}", options, Attribute.Description );
        }

        public virtual void BeginInit()
        {
            var value = Property.GetValue( Owner, null );
            var supportInit = value as ISupportInitialize;
            if ( supportInit != null )
            {
                supportInit.BeginInit();
            }
        }

        public virtual void EndInit()
        {
            var value = Property.GetValue( Owner, null );
            var supportInit = value as ISupportInitialize;
            if ( supportInit != null )
            {
                supportInit.EndInit();
            }
        }
    }
}

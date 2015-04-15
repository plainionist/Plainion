using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Plainion;

namespace Plainion.AppFw.Shell.Forms
{
    public class UserControl : IControl
    {
        public UserControl( object owner )
        {
            Contract.RequiresNotNull( owner, "owner" );

            Owner = owner;

            Controls = GetArgumentProperties();
        }

        private IEnumerable<IControl> GetArgumentProperties()
        {
            return Owner.GetType().GetProperties()
                 .Select( prop => ControlFactory.TryCreate( Owner, prop ) )
                 .Where( prop => prop != null )
                 .ToList();
        }

        public object Owner
        {
            get;
            private set;
        }

        public IEnumerable<IControl> Controls
        {
            get;
            private set;
        }

        public bool TryBind( string argument, Queue<string> additionalArguments )
        {
            return Controls.Any( child => child.TryBind( argument, additionalArguments ) );
        }

        public void Describe( TextWriter writer )
        {
            foreach ( var prop in Controls )
            {
                prop.Describe( Console.Out );
            }
        }
    
        public virtual void BeginInit()
        {
            var supportInit = Owner as ISupportInitialize;
            if ( supportInit != null )
            {
                supportInit.BeginInit();
            }

            foreach ( var child in Controls )
            {
                child.BeginInit();
            }
        }

        public virtual void EndInit()
        {
            var supportInit = Owner as ISupportInitialize;
            if ( supportInit != null )
            {
                supportInit.EndInit();
            }

            foreach ( var child in Controls )
            {
                child.EndInit();
            }
        }
    }
}

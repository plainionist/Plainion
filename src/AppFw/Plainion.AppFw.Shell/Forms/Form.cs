using System;
using System.Collections.Generic;
using System.Linq;
using Plainion.Collections;
using Plainion.Validation;

namespace Plainion.AppFw.Shell.Forms
{
    public class Form
    {
        public Form( object owner )
        {
            Content = new UserControl( owner );
        }

        public IControl Content
        {
            get;
            private set;
        }

        public void Bind( string[] args )
        {
            Content.BeginInit();

            BindArguments( args.ToQueue() );

            Content.EndInit();
        }

        public void Validate()
        {
            RecursiveValidator.Validate( Content.Owner );
        }

        private void BindArguments( Queue<string> args )
        {
            while ( args.Any() )
            {
                var arg = args.Dequeue();

                if ( !Content.TryBind( arg, args ) )
                {
                    throw new NotSupportedException( "Unknown argument: " + arg );
                }
            }
        }

        public void Usage()
        {
            Content.Describe( Console.Out );
        }
    }
}

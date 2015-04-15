using System;

namespace Plainion.AppFw.Shell
{
    public abstract class ActivityBase : IActivity
    {
        public string Message
        {
            get;
            set;
        }

        public void Execute( string[] args )
        {
            try
            {
                if ( !string.IsNullOrEmpty( Message ) )
                {
                    Console.WriteLine( Message );
                }

                ExecuteInternal( args );
            }
            catch ( Exception ex )
            {
                throw new ApplicationException( "Failed to execute: " + GetType(), ex );
            }
        }

        protected abstract void ExecuteInternal( string[] args );
    }
}

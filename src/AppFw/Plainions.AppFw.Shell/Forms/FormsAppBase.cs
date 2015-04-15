using System;
using Plainion.Logging;

namespace Plainion.AppFw.Shell.Forms
{
    public abstract class FormsAppBase : ActivityBase
    {
        private static readonly ILogger myLogger = LoggerFactory.GetLogger( typeof( FormsAppBase ) );
       
        [Argument( Short = "-h", Long = "--help", Description = "Prints usage information" )]
        public bool Help
        {
            get;
            set;
        }

        protected sealed override void ExecuteInternal( string[] args )
        {
            try
            {
                var form = new Form( this );
                form.Bind( args );

                if ( Help )
                {
                    form.Usage();
                    return;
                }

                form.Validate();

                Run();
            }
            catch ( Exception ex )
            {
                myLogger.Error( ex, "Failed to execute {0}", GetType().Name );
            }
        }

        protected abstract void Run();
    }
}

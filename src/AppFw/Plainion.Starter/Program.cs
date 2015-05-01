using System;
using System.Diagnostics;
using Plainion.AppFw.Shell.Hosting;
using Plainion.Logging;

namespace Plainion.Starter
{
    public sealed class Program
    {
        private static readonly ILogger myLogger = LoggerFactory.GetLogger( typeof( Program ) );

        [STAThread]
        public static int Main( string[] args )
        {
            if( Environment.GetEnvironmentVariable( "PLAINION_DBG" ).IsTrue() )
            {
                Debugger.Launch();
            }

            try
            {
                var engine = new ScriptEngine();
                if( !engine.Run( args ) )
                {
                    engine.PrintUsage( Console.Out );
                    return 2;
                }
            }
            catch( Exception ex )
            {
                myLogger.Error( ex, "Processing failed" );

                return 1;
            }

            return 0;
        }
    }
}

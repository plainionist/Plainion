using System;
using System.Diagnostics;
using System.IO;
using Plainion.AppFw.Shell.Hosting;
using Plainion.Logging;
using Plainion.Xaml;

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
                var settings = GetSettings();

                var engine = new ScriptEngine( settings.ScriptDirectories );
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

        private static Settings GetSettings()
        {
            var location = typeof( Program ).Assembly.Location;
            var settingsFile = location + ".xaml";

            if( File.Exists( settingsFile ) )
            {
                return new ValidatingXamlReader().Read<Settings>( settingsFile );
            }

            return new Settings();
        }
    }
}

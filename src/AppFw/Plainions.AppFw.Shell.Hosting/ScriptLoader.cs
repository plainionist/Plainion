using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plainion.Logging;
using Plainion.Xaml;

namespace Plainion.AppFw.Shell.Hosting
{
    public class ScriptLoader
    {
        private static readonly ILogger myLogger = LoggerFactory.GetLogger( typeof( ScriptLoader ) );

        private List<Script> myScripts;

        public ScriptLoader()
        {
            myScripts = new List<Script>();
        }

        public IEnumerable<Script> Scripts
        {
            get { return myScripts; }
        }

        public void Load( string rootDirectory )
        {
            var scriptFiles = Directory.GetFiles( rootDirectory, "*.xaml", SearchOption.AllDirectories );

            var scripts = scriptFiles
                .Select( f => LoadScriptSafe( f ) )
                .Where( col => col != null )
                .SelectMany( col => col.Scripts )
                .ToList();

            myScripts.AddRange( scripts );
        }

        private ScriptCollection LoadScriptSafe( string file )
        {
            try
            {
                return LoadScript( file );
            }
            catch( Exception ex )
            {
                myLogger.Error( ex, "Failed to load script: {0}", file );
                return null;
            }
        }

        public ScriptCollection LoadScript( string file )
        {
            myLogger.Debug( "Processing starter script: {0}", file );

            var reader = new ValidatingXamlReader();
            var obj = reader.Read<object>( file );

            if( obj is Script )
            {
                var col = new ScriptCollection();
                col.Scripts.Add( ( Script )obj );
                return col;
            }
            else if( obj is ScriptCollection )
            {
                return ( ScriptCollection )obj;
            }
            else
            {
                throw new NotSupportedException( "Unknown root element: " + obj.GetType() );
            }
        }
    }
}

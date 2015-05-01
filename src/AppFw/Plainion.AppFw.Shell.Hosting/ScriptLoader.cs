using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Plainion.Logging;
using Plainion.Xaml;

namespace Plainion.AppFw.Shell.Hosting
{
    public class ScriptLoader
    {
        private static readonly ILogger myLogger = LoggerFactory.GetLogger( typeof( ScriptLoader ) );

        private string myRootDirectory;
        private List<Script> myScripts;

        public ScriptLoader( string rootDirectory )
        {
            Contract.RequiresNotNull( rootDirectory, "rootDirectory" );

            myRootDirectory = rootDirectory;

            myScripts = new List<Script>();
        }

        public IEnumerable<Script> Scripts
        {
            get { return myScripts; }
        }

        public void Load()
        {
            var scriptFiles = Directory.GetFiles( myRootDirectory, "*.xaml", SearchOption.AllDirectories );

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

            LoadDependentAssemblies( file );

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

        //         xmlns:s="clr-namespace:Plainion.Scripts.TestRunner;assembly=Plainion.Scripts" 
        private void LoadDependentAssemblies( string scriptFile )
        {
            var doc = XElement.Load( scriptFile );

            var assembliesToLoad = doc.Attributes()
                .Where( attr => attr.IsNamespaceDeclaration )
                .Select( attr => attr.Value )
                .Where( ns => ns.StartsWith( "clr-namespace:", StringComparison.OrdinalIgnoreCase ) )
                .Where( ns => ns.Contains( "assembly=", StringComparison.OrdinalIgnoreCase ) )
                .Select( ns => ns.Split( new[] { "assembly=" }, StringSplitOptions.RemoveEmptyEntries ).Last() )
                .ToList();

            foreach( var dep in assembliesToLoad )
            {
                var isAlreadyLoaded = AppDomain.CurrentDomain.GetAssemblies()
                    .Any( asm => Path.GetFileNameWithoutExtension( asm.Location ).Equals( dep, StringComparison.OrdinalIgnoreCase ) );

                if( isAlreadyLoaded )
                {
                    myLogger.Info( "Assembly already loaded: {0}", dep );
                    continue;
                }

                var assembly = Directory.EnumerateFiles( myRootDirectory, "*.dll", SearchOption.AllDirectories )
                    .FirstOrDefault( dll => Path.GetFileNameWithoutExtension( dll ).Equals( dep, StringComparison.OrdinalIgnoreCase ) );

                Contract.Invariant( assembly != null, "Assembly not found: {0}", dep );

                Assembly.LoadFrom( assembly );
            }
        }
    }
}

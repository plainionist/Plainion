using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plainion.Logging;
using Plainion.AppFw.Shell;
using Plainion;

namespace Plainion.AppFw.Shell.Hosting
{
    public class ScriptEngine
    {
        private static readonly ILogger myLogger = LoggerFactory.GetLogger( typeof( ScriptEngine ) );

        private bool myShowHelp = false;
        private ScriptLoader myScriptLoader;
        private List<string> myScriptArgs;
        private List<string> myScriptsToExecute;
        private IReadOnlyCollection<string> myScriptDirectories;

        public ScriptEngine( IReadOnlyCollection<string> scriptDirectories )
        {
            Contract.RequiresNotNull( scriptDirectories, "scriptDirectories" );

            myScriptDirectories = scriptDirectories;

            myScriptLoader = new ScriptLoader();
            myScriptArgs = new List<string>();
            myScriptsToExecute = new List<string>();
        }

        public bool Run( string[] args )
        {
            ParseCmdArgs( args );

            LoadStarterScripts();

            if( myShowHelp || !myScriptsToExecute.Any() )
            {
                return false;
            }

            RunScripts();

            return true;
        }

        private void LoadStarterScripts()
        {
            var validDirs = myScriptDirectories
                .Where( dir => Directory.Exists( dir ) );

            foreach( var dir in validDirs )
            {
                myScriptLoader.Load( dir );
            }
        }

        private void RunScripts()
        {
            if( !myScriptsToExecute.Any() )
            {
                myLogger.Info( "No script to execute. Skipping" );
                return;
            }

            foreach( var options in myScriptsToExecute )
            {
                var script = GetScriptByOption( options );
                script.Activity.Execute( myScriptArgs.ToArray() );
            }
        }

        private Script GetScriptByOption( string option )
        {
            var scripts = myScriptLoader.Scripts
                .Where( s => s.Option.Equals( option, StringComparison.OrdinalIgnoreCase ) )
                .ToList();

            if( scripts.Count == 0 )
            {
                throw new Exception( "Option not supported: " + option );
            }
            if( scripts.Count > 1 )
            {
                throw new Exception( "Multiple scripts found for option: " + option );

            }

            return scripts.Single();
        }

        private void ParseCmdArgs( string[] args )
        {
            if( args == null )
            {
                return;
            }

            myScriptsToExecute.Clear();

            LoggerFactory.LogLevel = LogLevel.Notice;

            for( int i = 0; i < args.Length; ++i )
            {
                string arg = args[ i ];

                if( !myScriptsToExecute.Any() )
                {
                    // process global arguments
                    if( arg == "-h" || arg == "--help" )
                    {
                        myShowHelp = true;
                    }
                    else if( arg == "-v" || arg == "--verbose" )
                    {
                        LoggerFactory.LogLevel = LogLevel.Info;
                    }
                    else if( arg == "-d" || arg == "--debug" )
                    {
                        LoggerFactory.LogLevel = LogLevel.Debug;
                    }
                    else
                    {
                        // first non-global argument is treated as script
                        myScriptsToExecute.Add( arg );
                    }
                }
                else
                {
                    // once a script has been identified all subsequent arguments belong to the script
                    myScriptArgs.Add( arg );
                }
            }
        }

        public void PrintUsage( TextWriter writer )
        {
            writer.WriteLine( @"Plainion.Tools.Starter.exe [options] <script> <script args>" );
            writer.WriteLine();
            writer.WriteLine( @"Global options:" );
            writer.WriteLine( @"  -h/--help              print usage" );
            writer.WriteLine( @"  -v/--verbose           run in verbose mode" );
            writer.WriteLine( @"  -d/--debug             run in debug mode" );
            writer.WriteLine();

            PrintScripts( writer );
        }

        private void PrintScripts( TextWriter writer )
        {
            writer.WriteLine( @"Scripts:" );
            writer.WriteLine();

            foreach( var script in myScriptLoader.Scripts.OrderBy( h => h.Option ) )
            {
                writer.WriteLine( "{0,-30} - {1}", script.Option, script.Description );
            }
        }
    }
}

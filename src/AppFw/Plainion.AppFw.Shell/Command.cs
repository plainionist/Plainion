using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using Plainion.Diagnostics;

namespace Plainion.AppFw.Shell
{
    public class Command : ActivityBase
    {
        public Command()
        {
            Home = string.Empty;
        }

        public string Home { get; set; }

        [Required]
        public string Executable { get; set; }

        public string Arguments { get; set; }

        protected override void ExecuteInternal( string[] cmdLineArgs )
        {
            var executable = Path.Combine( Home, Executable );
            executable = Environment.ExpandEnvironmentVariables( executable );

            var args = Arguments + " " + string.Join( " ", cmdLineArgs );
            var process = new ProcessStartInfo( executable, args );

            Processes.Execute( process, Console.Out, Console.Error );
        }
    }
}

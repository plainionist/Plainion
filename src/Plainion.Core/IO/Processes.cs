using System.Diagnostics;
using System.IO;

namespace Plainion.IO
{
    public class Processes
    {
        /// <summary>
        /// Executes a process and waits for its exit.
        /// </summary>
        public static int Execute( ProcessStartInfo info )
        {
            return Execute( info, null, null );
        }

        /// <summary>
        /// Executes a process and waits for its exit.
        /// </summary>
        public static int Execute( ProcessStartInfo info, TextWriter stdout, TextWriter stderr )
        {
            info.UseShellExecute = false;

            DataReceivedEventHandler outputRedirector = ( sender, eventArgs ) => stdout.WriteLine( eventArgs.Data );
            DataReceivedEventHandler errorRedirector = ( sender, eventArgs ) => stderr.WriteLine( eventArgs.Data );

            Process p = new Process();
            p.StartInfo = info;

            if( stdout != null )
            {
                info.RedirectStandardOutput = true;
                p.OutputDataReceived += outputRedirector;
            }
            if( stderr != null )
            {
                info.RedirectStandardError = true;
                p.ErrorDataReceived += errorRedirector;
            }

            p.Start();

            if( stdout != null )
            {
                p.BeginOutputReadLine();
            }

            if( stderr != null )
            {
                p.BeginErrorReadLine();
            }

            p.WaitForExit();

            if( stderr != null )
            {
                p.ErrorDataReceived -= errorRedirector;
            }
            if( stdout != null )
            {
                p.OutputDataReceived -= outputRedirector;
            }

            return p.ExitCode;
        }
    }
}

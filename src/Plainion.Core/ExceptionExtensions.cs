using System;
using System.IO;
using System.Reflection;

namespace Plainion
{
    /// <summary>
    /// Provides extension methods to <see cref="Exception"/> objects.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Dumps the given exception content to a string and returns it.
        /// </summary>
        public static string Dump( this Exception exception )
        {
            Contract.RequiresNotNull( exception, "exception" );

            using ( var writer = new StringWriter() )
            {
                DumpTo( exception, writer, 0 );
                return writer.ToString();
            }
        }

        /// <summary>
        /// Dumps the given exception content to the given TextWriter.
        /// </summary>
        public static void DumpTo(this Exception exception, TextWriter writer)
        {
            Contract.RequiresNotNull( exception, "exception" );

            DumpTo( exception, writer, 0 );
        }

        /// <summary>
        /// Dumps the given exception content to the given TextWriter.
        /// Content will be: exception type, message, exception.Data, StackTrace. 
        /// InnerException is handled recursively. ReflectionTypeLoadException is expanded and LoaderExceptions are dummed.
        /// </summary>
        private static void DumpTo(this Exception exception, TextWriter writer, int level)
        {
            string padding = " ".PadRight( level );
            writer.WriteLine( padding + exception.GetType() + ": " + exception.Message );
            writer.WriteLine( padding + "Context: " );
            foreach ( var key in exception.Data.Keys )
            {
                writer.WriteLine( string.Format( "{0}  {1}: {2}",
                    padding, key, exception.Data[ key ] ) );
            }
            writer.WriteLine( padding + "StackTrace: " + exception.StackTrace );

            if ( exception.InnerException != null )
            {
                writer.WriteLine( padding + "Inner exception was: " );

                DumpTo( exception.InnerException, writer, level + 2 );
            }
            if ( exception is ReflectionTypeLoadException )
            {
                var typeLoadEx = (ReflectionTypeLoadException)exception;

                foreach ( var loaderEx in typeLoadEx.LoaderExceptions )
                {
                    writer.WriteLine( padding + "Loader exception was: " );
                    DumpTo( loaderEx, writer, level + 2 );
                }
            }
        }

        /// <summary>
        /// Preserves the full stack trace before rethrowing an exception.
        /// </summary>
        /// <remarks>
        /// According to this post see http://weblogs.asp.net/fmarguerie/archive/2008/01/02/rethrowing-exceptions-and-preserving-the-full-call-stack-trace.aspx
        /// it is required to get the full stack trace in any case.
        /// </remarks>
        public static void PreserveStackTrace( this Exception exception )
        {
            Contract.RequiresNotNull( exception, "exception" );

            var preserveStackTrace = typeof( Exception ).GetMethod( "InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic );
            preserveStackTrace.Invoke( exception, null );
        }

        /// <summary>
        /// Adds a key/value pair to Exception.Data
        /// </summary>
        public static Exception AddContext( this Exception exception, string key, object value )
        {
            Contract.RequiresNotNull( exception, "exception" );

            exception.Data[ key ] = value;

            return exception;
        }
    }
}

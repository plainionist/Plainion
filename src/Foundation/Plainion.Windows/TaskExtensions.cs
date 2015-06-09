using System;
using System.Threading.Tasks;
using System.Windows;

namespace Plainion.Windows
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Rethrows exceptions in the Application UI thread.
        /// </summary>
        public static Task<T> RethrowExceptionsInUIThread<T>( this Task<T> task )
        {
            task.ContinueWith( t =>
            {
                var ex = t.Exception;
                // http://stackoverflow.com/questions/10448987/dispatcher-currentdispatcher-vs-application-current-dispatcher
                Application.Current.Dispatcher.BeginInvoke( new Action( () => { throw ex; } ) );
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously );

            return task;
        }

        /// <summary>
        /// Rethrows exceptions in the Application UI thread.
        /// </summary>
        public static Task RethrowExceptionsInUIThread( this Task task )
        {
            task.ContinueWith( t =>
            {
                var ex = t.Exception;
                // http://stackoverflow.com/questions/10448987/dispatcher-currentdispatcher-vs-application-current-dispatcher
                Application.Current.Dispatcher.BeginInvoke( new Action( () => { throw ex; } ) );
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously );

            return task;
        }
    }
}

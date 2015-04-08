using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blade.Tasks
{
    public static class Tasks
    {
        public static Task<T> StartSTATask<T>( Func<T> body )
        {
            var tcs = new TaskCompletionSource<T>();

            var thread = new Thread( () =>
            {
                try
                {
                    tcs.SetResult( body() );
                }
                catch( Exception e )
                {
                    tcs.SetException( e );
                }
            } );

            thread.SetApartmentState( ApartmentState.STA );
            thread.Start();

            return tcs.Task;
        }

        public static Task StartSTATask( Action body )
        {
            var tcs = new TaskCompletionSource<bool>();

            var thread = new Thread( () =>
            {
                try
                {
                    body();
                    tcs.SetResult( true );
                }
                catch( Exception e )
                {
                    tcs.SetException( e );
                }
            } );

            thread.SetApartmentState( ApartmentState.STA );
            thread.Start();

            return tcs.Task;
        }
    }
}

using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Plainion.Prism.Tests.Interactivity.Fakes
{
    class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnMainWindowClose;

            //new Bootstrapper().Run();

            Current.MainWindow = new Shell();
            Current.MainWindow.WindowState = WindowState.Minimized;
            Current.MainWindow.Show();
        }

        public static void Close()
        {
            Current.Dispatcher.Invoke(() =>
            {
                Current.Shutdown();

                // support further application creations in same AppDomain
                typeof(Application).GetField("_isShuttingDown", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, false);
                typeof(Application).GetField("_appCreatedInThisAppDomain", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, false);
            });
        }

        public static void WaitForBeingLoaded()
        {
            while (Current == null) Thread.Yield();

            // http://stackoverflow.com/questions/13026826/execute-command-after-view-is-loaded-wpf-mvvm
            Current.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle, new Action(() => { }));
        }

    }
}

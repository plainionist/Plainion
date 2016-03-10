using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Plainion.AppFw.Wpf.Infrastructure;
using Plainion.Prism.Interactivity.InteractionRequest;
using Plainion.Progress;
using Plainion.Windows.Controls;
using Prism.Interactivity.InteractionRequest;

namespace Plainion.AppFw.Wpf.ViewModels
{
    [Export( typeof( ProjectLifecycleViewModel<> ) )]
    public class ProjectLifecycleViewModel<TProject> : BindableBase where TProject : ProjectBase
    {
        private IProjectService<TProject> myProjectService;
        private bool myIsBusy;
        private IProgressInfo myCurrentProgress;
        private CancellationTokenSource myCTS;
        private object myLock = new object();

        [ImportingConstructor]
        public ProjectLifecycleViewModel( IProjectService<TProject> projectService )
        {
            myProjectService = projectService;

            myProjectService.ProjectChanged += OnProjectChanged;

            NewCommand = new DelegateCommand( () => OnNew( async: false ) );
            OpenCommand = new DelegateCommand( () => OnOpen( async: false ) );
            SaveCommand = new DelegateCommand( () => OnSave( async: false ), () => myProjectService.Project != null );

            NewAsyncCommand = new DelegateCommand( () => OnNew( async: true ) );
            OpenAsyncCommand = new DelegateCommand( () => OnOpen( async: true ) );
            SaveAsyncCommand = new DelegateCommand( () => OnSave( async: true ), () => myProjectService.Project != null );

            ClosingCommand = new DelegateCommand<CancelEventArgs>( OnClosing );
            CloseCommand = new DelegateCommand( OnClose );

            ClosingConfirmationRequest = new InteractionRequest<ExitWithoutSaveNotification>();
            OpenFileRequest = new InteractionRequest<OpenFileDialogNotification>();
            SaveFileRequest = new InteractionRequest<SaveFileDialogNotification>();
        }

        private void OnProjectChanged( TProject newProject )
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        public string ApplicationName { get; set; }

        public string FileFilter { get; set; }

        public int FileFilterIndex { get; set; }

        public string DefaultFileExtension { get; set; }

        public bool AutoSaveNewProject { get; set; }

        /// <summary>
        /// Indicate that async activity is running.
        /// </summary>
        public bool IsBusy
        {
            get { return myIsBusy; }
            set { SetProperty( ref myIsBusy, value ); }
        }

        /// <summary>
        /// Progress of async activity
        /// </summary>
        public IProgressInfo Progress
        {
            get { return myCurrentProgress; }
            set { SetProperty( ref myCurrentProgress, value ); }
        }

        public ICommand NewCommand { get; private set; }

        public ICommand NewAsyncCommand { get; private set; }

        private void OnNew( bool async )
        {
            var args = new CancelEventArgs( false );
            HandleUnsavedData( args );

            if( args.Cancel )
            {
                return;
            }

            if( !AutoSaveNewProject )
            {
                if( async )
                {
                    RunAsync( ( progress, cancel ) => myProjectService.CreateAsync( null, false, progress, cancel ) );
                }
                else
                {
                    myProjectService.Create( null, false );
                }
                return;
            }

            var notification = new SaveFileDialogNotification();
            notification.RestoreDirectory = true;
            notification.Filter = FileFilter;
            notification.FilterIndex = FileFilterIndex;
            notification.DefaultExt = DefaultFileExtension;

            SaveFileRequest.Raise( notification, n => { } );

            if( !notification.Confirmed )
            {
                return;
            }

            if( async )
            {
                RunAsync( ( progress, cancel ) =>
                    {
                        return myProjectService.CreateAsync( notification.FileName, true, progress, cancel );
                    } );
            }
            else
            {
                myProjectService.Create( notification.FileName, true );

                myProjectService.Save();
            }
        }

        private void RunAsync( Func<IProgress<IProgressInfo>, CancellationToken, Task> action )
        {
            lock( myLock )
            {
                if( myCTS != null )
                {
                    myCTS.Cancel();
                    myCTS = null;
                }

                IsBusy = true;
                var progress = new Progress<IProgressInfo>( pi => Progress = pi );

                var cts = new CancellationTokenSource();

                action( progress, cts.Token )
                    .ContinueWith( t =>
                    {
                        myCTS = null;
                        IsBusy = false;
                        Progress = null;
                    }, cts.Token, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext() );

                myCTS = cts;
            }
        }

        public ICommand OpenCommand { get; private set; }

        public ICommand OpenAsyncCommand { get; private set; }

        public InteractionRequest<OpenFileDialogNotification> OpenFileRequest { get; private set; }

        private void OnOpen( bool async )
        {
            var args = new CancelEventArgs( false );
            HandleUnsavedData( args );

            if( args.Cancel )
            {
                return;
            }

            var notification = new OpenFileDialogNotification();
            notification.RestoreDirectory = true;
            notification.Filter = FileFilter;
            notification.FilterIndex = FileFilterIndex;
            notification.DefaultExt = DefaultFileExtension;

            OpenFileRequest.Raise( notification,
                n =>
                {
                    if( n.Confirmed )
                    {
                        if( async )
                        {
                            RunAsync( ( progress, cancel ) =>
                            {
                                return myProjectService.LoadAsync( n.FileName, progress, cancel );
                            } );
                        }
                        else
                        {
                            myProjectService.Load( n.FileName );
                        }
                    }
                } );
        }

        public DelegateCommand SaveCommand { get; private set; }

        public DelegateCommand SaveAsyncCommand { get; private set; }

        public InteractionRequest<SaveFileDialogNotification> SaveFileRequest { get; private set; }

        private bool OnSave( bool async )
        {
            TextBoxBinding.ForceSourceUpdate();

            if( !myProjectService.Project.IsDirty )
            {
                return true;
            }

            if( string.IsNullOrEmpty( myProjectService.Project.Location ) )
            {
                var notification = new SaveFileDialogNotification();
                notification.RestoreDirectory = true;
                notification.Filter = FileFilter;
                notification.FilterIndex = FileFilterIndex;
                notification.DefaultExt = DefaultFileExtension;

                SaveFileRequest.Raise( notification, n =>
                {
                    if( n.Confirmed )
                    {
                        myProjectService.Project.Location = n.FileName;
                    }
                } );

                if( !notification.Confirmed )
                {
                    return false;
                }
            }

            if( async )
            {
                RunAsync( ( progress, cancel ) =>
                {
                    return myProjectService.SaveAsync( progress, cancel );
                } );
            }
            else
            {
                myProjectService.Save();
            }

            return true;
        }

        public ICommand ClosingCommand { get; private set; }

        public InteractionRequest<ExitWithoutSaveNotification> ClosingConfirmationRequest { get; private set; }

        private void OnClosing( CancelEventArgs eventArgs )
        {
            HandleUnsavedData( eventArgs );
        }

        private void HandleUnsavedData( CancelEventArgs eventArgs )
        {
            TextBoxBinding.ForceSourceUpdate();

            if( myProjectService.Project == null || !myProjectService.Project.IsDirty )
            {
                return;
            }

            var notification = new ExitWithoutSaveNotification();
            notification.Title = ApplicationName;

            ClosingConfirmationRequest.Raise( notification, c =>
                {
                    if( c.Response == ExitWithoutSaveNotification.ResponseType.Yes )
                    {
                        eventArgs.Cancel = !OnSave( async: false );
                    }
                    else if( c.Response == ExitWithoutSaveNotification.ResponseType.No )
                    {
                        eventArgs.Cancel = false;
                    }
                    else
                    {
                        eventArgs.Cancel = true;
                    }
                } );
        }

        public ICommand CloseCommand { get; private set; }

        private void OnClose()
        {
            var args = new CancelEventArgs( false );
            HandleUnsavedData( args );

            if( !args.Cancel )
            {
                Application.Current.Shutdown();
            }
        }
    }
}

using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Plainion.AppFw.Wpf.Model;
using Plainion.AppFw.Wpf.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Plainion.Prism.Interactivity.InteractionRequest;
using Plainion.Windows.Controls;

namespace Plainion.AppFw.Wpf.ViewModels
{
    [Export( typeof( ProjectLifecycleViewModel<> ) )]
    public class ProjectLifecycleViewModel<TProject> : BindableBase where TProject : ProjectBase
    {
        private IProjectService<TProject> myProjectService;
        private IPersistenceService<TProject> myPersistenceService;

        [ImportingConstructor]
        public ProjectLifecycleViewModel( IProjectService<TProject> projectService, IPersistenceService<TProject> persistenceService )
        {
            myProjectService = projectService;
            myPersistenceService = persistenceService;

            myProjectService.ProjectChanged += OnProjectChanged;

            NewCommand = new DelegateCommand( OnNew );
            OpenCommand = new DelegateCommand( OnOpen );
            SaveCommand = new DelegateCommand( () => OnSave(), () => myProjectService.Project != null );
            ClosingCommand = new DelegateCommand<CancelEventArgs>( OnClosing );
            CloseCommand = new DelegateCommand( OnClose );

            ClosingConfirmationRequest = new InteractionRequest<ExitWithoutSaveNotification>();
            OpenFileRequest = new InteractionRequest<OpenFileDialogNotification>();
            SaveFileRequest = new InteractionRequest<SaveFileDialogNotification>();
        }

        private void OnProjectChanged( object sender, ProjectChangeEventArgs<TProject> e )
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        public string ApplicationName { get; set; }

        public string FileFilter { get; set; }

        public int FileFilterIndex { get; set; }

        public string DefaultFileExtension { get; set; }

        public bool AutoSaveNewProject { get; set; }

        public ICommand NewCommand { get; private set; }

        private void OnNew()
        {
            var args = new CancelEventArgs( false );
            HandleUnsavedData( args );

            if ( args.Cancel )
            {
                return;
            }

            if ( !AutoSaveNewProject )
            {
                myProjectService.Project = myProjectService.CreateEmptyProject( null );
                return;
            }

            var notification = new SaveFileDialogNotification();
            notification.RestoreDirectory = true;
            notification.Filter = FileFilter;
            notification.FilterIndex = FileFilterIndex;
            notification.DefaultExt = DefaultFileExtension;

            SaveFileRequest.Raise( notification, n => { } );

            if ( !notification.Confirmed )
            {
                return;
            }

            var project = myProjectService.CreateEmptyProject( notification.FileName );

            myPersistenceService.Save( project );

            myProjectService.Project = project;
        }

        public ICommand OpenCommand { get; private set; }

        public InteractionRequest<OpenFileDialogNotification> OpenFileRequest { get; private set; }

        private void OnOpen()
        {
            var args = new CancelEventArgs( false );
            HandleUnsavedData( args );

            if ( args.Cancel )
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
                    if ( n.Confirmed )
                    {
                        myProjectService.Project = myPersistenceService.Load( n.FileName );
                    }
                } );
        }

        public DelegateCommand SaveCommand { get; private set; }

        public InteractionRequest<SaveFileDialogNotification> SaveFileRequest { get; private set; }

        private bool OnSave()
        {
            TextBoxBinding.ForceSourceUpdate();

            if ( !myProjectService.Project.IsDirty )
            {
                return true;
            }

            if ( string.IsNullOrEmpty( myProjectService.Project.Location ) )
            {
                var notification = new SaveFileDialogNotification();
                notification.RestoreDirectory = true;
                notification.Filter = FileFilter;
                notification.FilterIndex = FileFilterIndex;
                notification.DefaultExt = DefaultFileExtension;

                SaveFileRequest.Raise( notification, n =>
                {
                    if ( n.Confirmed )
                    {
                        myProjectService.Project.Location = n.FileName;
                    }
                } );

                if ( !notification.Confirmed )
                {
                    return false;
                }
            }

            myPersistenceService.Save( myProjectService.Project );

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

            if ( myProjectService.Project == null || !myProjectService.Project.IsDirty )
            {
                return;
            }

            var notification = new ExitWithoutSaveNotification();
            notification.Title = ApplicationName;

            ClosingConfirmationRequest.Raise( notification, c =>
                {
                    if ( c.Response == ExitWithoutSaveNotification.ResponseType.Yes )
                    {
                        eventArgs.Cancel = !OnSave();
                    }
                    else if ( c.Response == ExitWithoutSaveNotification.ResponseType.No )
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

            if ( !args.Cancel )
            {
                Application.Current.Shutdown();
            }
        }
    }
}

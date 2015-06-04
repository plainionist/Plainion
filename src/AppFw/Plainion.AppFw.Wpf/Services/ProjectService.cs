using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Plainion.AppFw.Wpf.Infrastructure;
using Plainion.Progress;

namespace Plainion.AppFw.Wpf.Services
{
    public abstract class ProjectService<TProject> : IProjectService<TProject> where TProject : ProjectBase, new()
    {
        private TProject myProject;

        public TProject Project
        {
            get { return myProject; }
            private set
            {
                if( object.ReferenceEquals( myProject, value ) )
                {
                    return;
                }

                OnProjectChanging( myProject );

                myProject = value;

                OnProjectChanged( myProject );
            }
        }

        protected virtual void OnProjectChanging( TProject oldProject )
        {
            if( ProjectChanging != null )
            {
                ProjectChanging( oldProject );
            }
        }

        protected virtual void OnProjectChanged( TProject newProject )
        {
            if( ProjectChanged != null )
            {
                ProjectChanged( newProject );
            }
        }

        public event Action<TProject> ProjectChanging;
        public event Action<TProject> ProjectChanged;

        public void Create( string location )
        {
            var project = new TProject();
            project.IsDirty = false;
            project.Location = location;

            InitializeProject( project, new NullProgress(), default( CancellationToken ) );

            Project = project;
        }

        protected virtual void InitializeProject( TProject project, IProgress<IProgressInfo> progress, CancellationToken cancellationToken )
        {
        }

        public Task CreateAsync( string location, IProgress<IProgressInfo> progress, CancellationToken cancellationToken )
        {
            return Task.Run<TProject>( () =>
            {
                var project = new TProject();
                project.IsDirty = false;
                project.Location = location;

                InitializeProject( project, progress, cancellationToken );

                return project;
            }, cancellationToken )
            .ContinueWith( t => Project = t.Result,
                cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext() );
        }

        public void Load( string location )
        {
            var project = Deserialize( location, new NullProgress(), default( CancellationToken ) );

            project.Location = location;
            project.IsDirty = false;

            Project = project;
        }

        protected abstract TProject Deserialize( string location, IProgress<IProgressInfo> progress, CancellationToken cancellationToken );

        public Task LoadAsync( string location, IProgress<IProgressInfo> progress, CancellationToken cancellationToken )
        {
            return Task.Run<TProject>( () =>
            {
                var project = Deserialize( location, progress, cancellationToken );

                project.Location = location;
                project.IsDirty = false;

                return project;
            }, cancellationToken )
            .ContinueWith( t => Project = t.Result,
                cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext() );
        }

        public void Save()
        {
            Serialize( Project, new NullProgress(), default( CancellationToken ) );

            Project.IsDirty = false;
        }

        protected abstract void Serialize( TProject project, IProgress<IProgressInfo> progress, CancellationToken cancellationToken );

        public Task SaveAsync( IProgress<IProgressInfo> progress, CancellationToken cancellationToken )
        {
            return Task.Run( () =>
            {
                Serialize( Project, progress, cancellationToken );
            }, cancellationToken )
            .ContinueWith( t => Project.IsDirty = false,
                cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext() );
        }
    }
}

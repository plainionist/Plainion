using System;
using Plainion.AppFw.Wpf.Infrastructure;

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

        public virtual void CreateEmptyProject( string location )
        {
            var project = new TProject();
            project.IsDirty = false;
            project.Location = location;

            Project = project;
        }

        public void Load( string file )
        {
            var project = Deserialize( file );

            project.Location = file;
            project.IsDirty = false;

            Project = project;
        }

        protected abstract void Serialize( TProject project );

        public void Save()
        {
            Serialize( Project );

            Project.IsDirty = false;
        }

        protected abstract TProject Deserialize( string file );
    }
}

using System;
using System.ComponentModel.Composition;
using Plainion.AppFw.Wpf.Model;

namespace Plainion.AppFw.Wpf.Services
{
    public class ProjectService<TProject> : IProjectService<TProject> where TProject : ProjectBase, new()
    {
        private TProject myProject;

        public TProject Project
        {
            get { return myProject; }
            set
            {
                if ( object.ReferenceEquals( myProject, value ) )
                {
                    return;
                }

                var args = new ProjectChangeEventArgs<TProject>( myProject, value );

                OnProjectChanging( args );

                myProject = value;

                OnProjectChanged( args );
            }
        }

        protected virtual void OnProjectChanging( ProjectChangeEventArgs<TProject> args )
        {
            if ( ProjectChanging != null )
            {
                ProjectChanging( this, args );
            }
        }

        protected virtual void OnProjectChanged( ProjectChangeEventArgs<TProject> args )
        {
            if ( ProjectChanged != null )
            {
                ProjectChanged( this, args );
            }
        }

        public event EventHandler<ProjectChangeEventArgs<TProject>> ProjectChanging;
        public event EventHandler<ProjectChangeEventArgs<TProject>> ProjectChanged;

        public virtual TProject CreateEmptyProject( string location )
        {
            var project = new TProject();
            project.IsDirty = false;
            project.Location = location;

            return project;
        }
    }
}

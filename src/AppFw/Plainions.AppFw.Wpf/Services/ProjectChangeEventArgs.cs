using System;
using Plainion.AppFw.Wpf.Model;

namespace Plainion.AppFw.Wpf.Services
{
    public class ProjectChangeEventArgs<TProject> : EventArgs where TProject : ProjectBase
    {
        public ProjectChangeEventArgs( TProject oldProject, TProject newProject )
        {
            OldProject = oldProject;
            NewProject = newProject;
        }

        public TProject OldProject
        {
            get;
            private set;
        }

        public TProject NewProject
        {
            get;
            private set;
        }
    }
}

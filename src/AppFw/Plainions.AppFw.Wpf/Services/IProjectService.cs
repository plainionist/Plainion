using System;
using System.ComponentModel.Composition;
using Plainion.AppFw.Wpf.Model;

namespace Plainion.AppFw.Wpf.Services
{
    [InheritedExport( typeof( IProjectService<> ) )]
    public interface IProjectService<TProject> where TProject : ProjectBase
    {
        TProject Project { get; set; }

        event EventHandler<ProjectChangeEventArgs<TProject>> ProjectChanging;
        event EventHandler<ProjectChangeEventArgs<TProject>> ProjectChanged;

        TProject CreateEmptyProject( string location );
    }
}

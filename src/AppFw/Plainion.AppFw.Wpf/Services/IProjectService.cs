using System;
using System.ComponentModel.Composition;
using Plainion.AppFw.Wpf.Model;

namespace Plainion.AppFw.Wpf.Services
{
    [InheritedExport( typeof( IProjectService<> ) )]
    public interface IProjectService<TProject> where TProject : ProjectBase
    {
        TProject Project { get; }

        event Action<TProject> ProjectChanging;
        event Action<TProject> ProjectChanged;

        void CreateEmptyProject( string location );

        void Load( string file );
        void Save();
    }
}

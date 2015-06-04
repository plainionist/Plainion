using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Plainion.Progress;

namespace Plainion.AppFw.Wpf.Infrastructure
{
    [InheritedExport( typeof( IProjectService<> ) )]
    public interface IProjectService<TProject> where TProject : ProjectBase
    {
        TProject Project { get; }

        event Action<TProject> ProjectChanging;
        event Action<TProject> ProjectChanged;

        void Create( string location, bool autoSave );
        void Load( string location );
        void Save();

        Task CreateAsync( string location, bool autoSave, IProgress<IProgressInfo> progress, CancellationToken cancellationToken );
        Task LoadAsync( string location, IProgress<IProgressInfo> progress, CancellationToken cancellationToken );
        Task SaveAsync( IProgress<IProgressInfo> progress, CancellationToken cancellationToken );
    }
}

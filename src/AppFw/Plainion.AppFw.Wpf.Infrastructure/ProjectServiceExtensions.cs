using System;
using System.Threading;
using System.Threading.Tasks;
using Plainion.Progress;

namespace Plainion.AppFw.Wpf.Infrastructure
{
    public static class ProjectServiceExtensions
    {
        public static Task CreateAsync<TProject>( this IProjectService<TProject> self, string location, bool autoSave )
            where TProject : ProjectBase
        {
            return self.CreateAsync( location, autoSave, new NullProgress() );
        }

        public static Task CreateAsync<TProject>( this IProjectService<TProject> self, string location, bool autoSave, IProgress<IProgressInfo> progress )
            where TProject : ProjectBase
        {
            return self.CreateAsync( location, autoSave, progress, default( CancellationToken ) );
        }

        public static Task LoadAsync<TProject>( this IProjectService<TProject> self, string location )
            where TProject : ProjectBase
        {
            return self.LoadAsync( location, new NullProgress() );
        }

        public static Task LoadAsync<TProject>( this IProjectService<TProject> self, string location, IProgress<IProgressInfo> progress )
            where TProject : ProjectBase
        {
            return self.LoadAsync( location, progress, default( CancellationToken ) );
        }

        public static Task SaveAsync<TProject>( this IProjectService<TProject> self )
            where TProject : ProjectBase
        {
            return self.SaveAsync( new NullProgress() );
        }

        public static Task SaveAsync<TProject>( this IProjectService<TProject> self, IProgress<IProgressInfo> progress )
            where TProject : ProjectBase
        {
            return self.SaveAsync( progress, default( CancellationToken ) );
        }
    }
}

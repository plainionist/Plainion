using System.ComponentModel.Composition;
using Plainion.AppFw.Wpf.Model;

namespace Plainion.AppFw.Wpf.Services
{
    [InheritedExport( typeof( IPersistenceService<> ) )]
    public interface IPersistenceService<TProject> where TProject : ProjectBase
    {
        TProject Load( string file );
        void Save( TProject project );
    }
}

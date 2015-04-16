using System.ComponentModel.Composition;
using Plainion.AppFw.Wpf.Model;

namespace Plainion.AppFw.Wpf.Services
{
    [InheritedExport( typeof( ISerializer<> ) )]
    public interface ISerializer<TProject> where TProject : ProjectBase
    {
        void Serialize( TProject project );
        TProject Deserialize( string file );
    }
}

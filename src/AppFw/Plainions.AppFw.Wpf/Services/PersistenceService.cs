using System.ComponentModel.Composition;
using Plainion.AppFw.Wpf.Model;

namespace Plainion.AppFw.Wpf.Services
{
    public sealed class PersistenceService<TProject> : IPersistenceService<TProject> where TProject : ProjectBase
    {
        private ExportFactory<ISerializer<TProject>> mySerializerFactory;

        [ImportingConstructor]
        public PersistenceService( ExportFactory<ISerializer<TProject>> serializerFactory )
        {
            mySerializerFactory = serializerFactory;
        }

        public TProject Load( string file )
        {
            var serializer = mySerializerFactory.CreateExport().Value;

            var project = serializer.Deserialize( file );

            project.Location = file;
            project.IsDirty = false;

            return project;
        }

        public void Save( TProject project )
        {
            var serializer = mySerializerFactory.CreateExport().Value;
            serializer.Serialize( project );

            project.IsDirty = false;
        }
    }
}

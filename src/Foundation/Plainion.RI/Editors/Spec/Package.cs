using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;

namespace Plainion.RI.Editors.Spec
{
    [ContentProperty( "Patterns" )]
    public class Package : PackageBase
    {
        public Package()
        {
            Clusters = new List<Cluster>();
        }

        [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
        public List<Cluster> Clusters { get; private set; }
    }
}

using System.Collections.Generic;
using System.Windows.Markup;

namespace Plainion.AppFw.Shell
{
    [ContentProperty( "Scripts" )]
    public class ScriptCollection
    {
        public ScriptCollection()
        {
            Scripts = new List<Script>();
        }

        public List<Script> Scripts
        {
            get;
            set;
        }
    }
}

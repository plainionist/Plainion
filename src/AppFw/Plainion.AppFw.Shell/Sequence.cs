using System.Collections.Generic;
using System.Windows.Markup;

namespace Plainion.AppFw.Shell
{
    [ContentProperty( "Children" )]
    public class Sequence : ActivityBase
    {
        private IList<KeyValuePair<IActivity, string>> myMessages;

        public Sequence()
        {
            Children = new List<IActivity>();
            myMessages = new List<KeyValuePair<IActivity, string>>();
        }

        public List<IActivity> Children
        {
            get;
            private set;
        }

        /// <summary>
        /// Command line arguments are NOT passed to the scripts. Script arguments can only be configured via script Xaml
        /// </summary>
        protected override void ExecuteInternal( string[] args )
        {
            foreach ( var child in Children )
            {
                child.Execute( new string[] { } );
            }
        }
    }
}

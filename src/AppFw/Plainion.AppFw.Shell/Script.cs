using System.Windows.Markup;

namespace Plainion.AppFw.Shell
{
    [ContentProperty( "Activity" )]
    public class Script
    {
        public string Option
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public IActivity Activity
        {
            get;
            set;
        }
    }
}

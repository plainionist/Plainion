
namespace Plainion.AppFw.Shell
{
    public interface IActivity
    {
        string Message
        {
            get;
            set;
        }

        void Execute( string[] args );
    }
}

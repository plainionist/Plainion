
namespace Plainion
{
    /// <summary>
    /// Defines a progress without value.
    /// This implementation is usually used if a progress from a third party async activity should be reported where
    /// detailed updates are not provided but a description can be given to the user.
    /// </summary>
    public struct UndefinedProgress : IProgressInfo
    {
        public UndefinedProgress( string activity )
            : this()
        {
            Activity = activity;
            Progress = -1;
        }

        public string Activity { get; private set; }

        public double Progress { get; private set; }
    
        public string Details { get; set; }
    }
}

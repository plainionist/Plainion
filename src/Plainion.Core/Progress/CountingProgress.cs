
namespace Plainion
{
    /// <summary>
    /// Defines a progress which starts at 1 and just increases without defined maximum.
    /// </summary>
    public struct CountingProgress : IProgressInfo
    {
        public CountingProgress( string activity )
            : this()
        {
            Activity = activity;
        }

        public string Activity { get; private set; }

        public void IncrementBy( int value )
        {
            Contract.Requires( value > 0, "value must be > 0" );
            Progress += value;
        }

        public double Progress { get; private set; }
    
        public string Details { get; set; }
    }
}

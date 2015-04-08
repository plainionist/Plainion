
namespace Plainion
{
    /// <summary>
    /// Defines a progress which gives percentage updates.
    /// </summary>
    public struct PercentageProgress : IProgressInfo
    {
        public PercentageProgress( string activity, double maximum )
            : this()
        {
            Contract.Requires( maximum > 0, "Maximum must be > 0" );

            Activity = activity;
            Maximum = maximum;
        }

        public string Activity { get; private set; }

        public double Maximum { get; private set; }

        public double Value { get; set; }

        public double Progress { get { return Value / Maximum * 100; } }
    
        public string Details { get; set; }
    }
}

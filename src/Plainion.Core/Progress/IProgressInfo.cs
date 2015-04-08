using System;

namespace Plainion
{
    /// <summary>
    /// Provides simple progress description to be used with System.IProgress
    /// </summary>
    public interface IProgressInfo 
    {
        /// <summary>
        /// The activity going on which progress is reported.
        /// </summary>
        string Activity { get; }

        /// <summary>
        /// Progress value.
        /// </summary>
        double Progress { get; }
    
        /// <summary>
        /// Additional details describing the activity or steps of it.
        /// </summary>
        string Details { get; }
    }
}

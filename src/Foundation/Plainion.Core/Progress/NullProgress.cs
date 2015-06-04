using System;

namespace Plainion.Progress
{
    /// <summary>
    /// Null-object pattern for IProgress{T}
    /// </summary>
    public sealed class NullProgress<T> : IProgress<T>
    {
        public void Report( T value ) { }
    }

    /// <summary>
    /// Null-object pattern for IProgress{IProgressInfo}
    /// </summary>
    public sealed class NullProgress : IProgress<IProgressInfo>
    {
        public void Report( IProgressInfo value ) { }
    }
}

using System.Collections.Generic;
using Plainion.Collections;

namespace Plainion.Diagnostics
{
    /// <summary>
    /// Provides an <see cref="Index{K,V}"/> for process to thread collections
    /// </summary>
    public class ProcessThreadSet : Index<int, HashSet<int>>
    {
        public ProcessThreadSet()
            : base( pid => new HashSet<int>() )
        {
        }
    }
}

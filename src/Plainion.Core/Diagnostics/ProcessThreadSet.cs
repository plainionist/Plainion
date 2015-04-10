using System.Collections.Generic;
using Plainion.Collections;

namespace Plainion.Diagnostics
{
    public class ProcessThreadSet : Index<int, HashSet<int>>
    {
        public ProcessThreadSet()
            : base( pid => new HashSet<int>() )
        {
        }
    }
}

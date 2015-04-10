using System;
using System.Collections.Generic;
using System.Linq;
using Plainion.Collections;

namespace Plainion.Diagnostics
{
    public class ProcessThreadIndex<TValue> : Index<int, Index<int, TValue>>
    {
        public ProcessThreadIndex( Func<int, int, TValue> valueCreator )
            : base( pid => new Index<int, TValue>( tid => valueCreator( pid, tid ) ) )
        {
        }

        public new IEnumerable<TValue> Values
        {
            get { return base.Values.SelectMany( v => v.Values ); }
        }
    }
}

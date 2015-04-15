using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Plainion.AppFw.Shell.Forms
{
    public interface IControl : ISupportInitialize
    {
        object Owner { get; }
        bool TryBind( string argument, Queue<string> additionalArguments );
        void Describe( TextWriter writer );
    }
}

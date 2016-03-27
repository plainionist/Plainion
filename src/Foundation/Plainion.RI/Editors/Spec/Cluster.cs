﻿using System.Linq;
using System.Windows.Markup;

namespace Plainion.RI.Editors.Spec
{
    [ContentProperty( "Patterns" )]
    public class Cluster : PackageBase
    {
        internal bool Matches(string str)
        {
            return Includes.Any(i => i.Matches(str)) && !Excludes.Any(e => e.Matches(str));
        }
    }
}

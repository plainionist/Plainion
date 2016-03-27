﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;

namespace Plainion.RI.Editors.Spec
{
    [ContentProperty("Packages")]
    public class SystemPackaging
    {
        public SystemPackaging()
        {
            Packages = new List<Package>();
        }
        
        public string AssemblyRoot { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<Package> Packages { get; private set; }
    }
}

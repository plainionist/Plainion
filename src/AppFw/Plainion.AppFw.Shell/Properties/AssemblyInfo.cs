using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Markup;

[assembly: AssemblyTitle( "Plainion.AppFw.Shell" )]
[assembly: CLSCompliant( true )]
[assembly: AssemblyVersion( "1.3.0.0" )]

// required since .NET 4.0 to allow calls to other assemblies
[assembly: AllowPartiallyTrustedCallersAttribute()]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly : ComVisible( false )]

[assembly: XmlnsPrefix( "http://github.com/ronin4net/plainion/appfw/shell", "pnfw" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion/appfw/shell", "Plainion.AppFw.Shell" )]


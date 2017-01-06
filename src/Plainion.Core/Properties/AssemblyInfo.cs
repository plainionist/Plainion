
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AssemblyTitle( "Plainion.Core" )]
[assembly: CLSCompliant( true )]
[assembly: AssemblyVersion( "3.0.0.0" )]

// required since .NET 4.0 to allow calls to other assemblies
[assembly: AllowPartiallyTrustedCallersAttribute()]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly : ComVisible( false )]


[assembly: InternalsVisibleTo( "Plainion.Core.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100656259005e9fc8444ec8025c25d3bbdfb44b2dddd280bcb4fe9f5898d53727b5510c943c68bba6a3ad44014f118b22b0a23b45304773c68870b82ee23677a91574674cb7d73fc2b2cb8dd46f9ec01e4486c3d9ad8134af3bdc1d8e4165b88f226af62f2977ec4735f65a62176ad84b4605a9ab1f0d95050ec1e8f55a5ca513e7" )]



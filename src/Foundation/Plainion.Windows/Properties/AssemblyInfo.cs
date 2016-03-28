using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

[assembly: AssemblyTitle( "Plainion.Windows" )]
[assembly: ComVisible( false )]
[assembly: AssemblyVersion( "1.12.0.0" )]

[assembly: XmlnsPrefix( "http://github.com/ronin4net/plainion", "pn" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows.Controls" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows.Controls.Tree" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows.Interactivity" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows.Interactivity.DragDrop" )]

// sn.exe -Tp <assembly>
[assembly: InternalsVisibleTo( "Plainion.Windows.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100656259005e9fc8444ec8025c25d3bbdfb44b2dddd280bcb4fe9f5898d53727b5510c943c68bba6a3ad44014f118b22b0a23b45304773c68870b82ee23677a91574674cb7d73fc2b2cb8dd46f9ec01e4486c3d9ad8134af3bdc1d8e4165b88f226af62f2977ec4735f65a62176ad84b4605a9ab1f0d95050ec1e8f55a5ca513e7" )]

// Specifies the location in which theme dictionaries are stored for types in an assembly.
[assembly: ThemeInfo(
    // Specifies the location of system theme-specific resource dictionaries for this project.
    // The default setting in this project is "None" since this default project does not
    // include these user-defined theme files:
    //     Themes\Aero.NormalColor.xaml
    //     Themes\Classic.xaml
    //     Themes\Luna.Homestead.xaml
    //     Themes\Luna.Metallic.xaml
    //     Themes\Luna.NormalColor.xaml
    //     Themes\Royale.NormalColor.xaml
    ResourceDictionaryLocation.None,

    // Specifies the location of the system non-theme specific resource dictionary:
    //     Themes\generic.xaml
    ResourceDictionaryLocation.SourceAssembly )]



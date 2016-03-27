using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

[assembly: AssemblyTitle("Plainion.Windows.Editors")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion( "1.0.0.0" )]

[assembly: XmlnsPrefix( "http://github.com/ronin4net/plainion", "pn" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows.Editors" )]
[assembly: XmlnsDefinition( "http://github.com/ronin4net/plainion", "Plainion.Windows.Editors.Xml" )]

[assembly:ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                             //(used if a resource is not found in the page, 
                             // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                      //(used if a resource is not found in the page, 
                                      // app, or any theme specific resource dictionaries)
)]

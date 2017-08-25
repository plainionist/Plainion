using System.IO;
using NUnit.Framework;
using Plainion.Xaml;

namespace Plainion.Tests.Xaml
{
    [TestFixture]
    class IncludeTests
    {
        [Test,Ignore("we currently have no idea how to specify relative paths - relative to incuding file")]
        public void Include_FileExists_XamlIncluded()
        {
            var master = Path.Combine( Path.GetDirectoryName( GetType().Assembly.Location ), "TestData/Xaml/Master.xaml" );

            // ValidatingXamlReader only used for convenience. .Net XamlReader would do as well
            var model = new ValidatingXamlReader().Read<Model>( master );

            Assert.That( model.Content.Value, Is.EqualTo( 42 ) );
        }
    }
}

using System.Collections.Generic;
using NUnit.Framework;
using Plainion.AppFw.Shell.Forms;
using Plainion.Collections;

namespace Plainion.AppFw.Shell.UnitTests
{
    [TestFixture]
    public class GenericControlTests
    {
        [Test]
        public void TryBind_WithBool_PropertyIsTrue()
        {
            var script = new TestApp();

            var prop = ControlFactory.TryCreate( script, script.GetType().GetProperty( "BoolArgument" ) );
            var success = prop.TryBind( "-B", new Queue<string>() );

            Assert.That( success, Is.True );
            Assert.That( script.BoolArgument, Is.True );
        }

        [Test]
        public void TryBind_WithString_PropertyReturnsValue()
        {
            var script = new TestApp();

            var prop = ControlFactory.TryCreate( script, script.GetType().GetProperty( "StringArgument" ) );
            var success = prop.TryBind( "-S", new string[] { "test" }.ToQueue() );

            Assert.That( success, Is.True );
            Assert.That( script.StringArgument, Is.EqualTo( "test" ) );
        }

        [Test]
        public void TryBind_WithList_ValueAdded()
        {
            var script = new TestApp();

            var prop = ControlFactory.TryCreate( script, script.GetType().GetProperty( "ListArgument" ) );
            var success = prop.TryBind( "-L", new string[] { "a" }.ToQueue() );

            Assert.That( success, Is.True );
            Assert.That( script.ListArgument.Contains( "a" ), Is.True );
        }

        [Test]
        public void TryBind_WithDictionary_KeyAndValueSet()
        {
            var script = new TestApp();

            var prop = ControlFactory.TryCreate( script, script.GetType().GetProperty( "DictionaryArgument" ) );
            var success = prop.TryBind( "-D", new string[] { "a=b" }.ToQueue() );

            Assert.That( success, Is.True );
            Assert.That( script.DictionaryArgument.ContainsKey( "a" ), Is.True );
            Assert.That( script.DictionaryArgument[ "a" ], Is.EqualTo( "b" ) );
        }
    }
}

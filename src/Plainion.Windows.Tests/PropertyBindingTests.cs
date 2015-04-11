using System;
using System.ComponentModel;
using System.Windows.Data;
using NUnit.Framework;
using Plainion.Windows.Tests.Fakes;

namespace Plainion.Windows.Tests
{
    [TestFixture]
    public class PropertyBindingTests
    {
        [TestCase]
        public void TwoWayBinding_ChangesOnEitherEndSyncedToTheOtherOne()
        {
            var vm1 = new ViewModel1();
            var vm2 = new ViewModel2();

            PropertyBinding.Bind( () => vm1.PrimaryValue, () => vm2.SecondaryValue, BindingMode.TwoWay );

            vm1.PrimaryValue = 42;
            Assert.That( vm2.SecondaryValue, Is.EqualTo( 42 ) );

            vm2.SecondaryValue = 24;
            Assert.That( vm1.PrimaryValue, Is.EqualTo( 24 ) );
        }

        [TestCase]
        public void OneWayBinding_ChangesOnSourceSyncedToTarget()
        {
            var vm1 = new ViewModel1();
            var vm2 = new ViewModel2();

            PropertyBinding.Bind( () => vm1.PrimaryValue, () => vm2.SecondaryValue, BindingMode.OneWay );

            vm1.PrimaryValue = 42;
            Assert.That( vm2.SecondaryValue, Is.EqualTo( 42 ) );
        }

        [TestCase]
        public void OneWayBinding_ChangesOnTargetNotSyncedtoSource()
        {
            var vm1 = new ViewModel1();
            var vm2 = new ViewModel2();

            PropertyBinding.Bind( () => vm1.PrimaryValue, () => vm2.SecondaryValue, BindingMode.OneWay );

            vm2.SecondaryValue = 42;
            Assert.That( vm1.PrimaryValue, Is.Not.EqualTo( 42 ) );
        }

        [TestCase]
        public void OneWayToSourceBinding_ChangesOnSourceSyncedToTarget()
        {
            var vm1 = new ViewModel1();
            var vm2 = new ViewModel2();

            PropertyBinding.Bind( () => vm1.PrimaryValue, () => vm2.SecondaryValue, BindingMode.OneWayToSource );

            vm2.SecondaryValue = 42;
            Assert.That( vm1.PrimaryValue, Is.EqualTo( 42 ) );
        }

        [TestCase]
        public void OneWayToSourceBinding_ChangesOnTargetNotSyncedtoSource()
        {
            var vm1 = new ViewModel1();
            var vm2 = new ViewModel2();

            PropertyBinding.Bind( () => vm1.PrimaryValue, () => vm2.SecondaryValue, BindingMode.OneWayToSource );

            vm1.PrimaryValue = 42;
            Assert.That( vm2.SecondaryValue, Is.Not.EqualTo( 42 ) );
        }

        [TestCase]
        public void TwoWayBinding_SourceAndTargetMarkedForGC_BindingReleased()
        {
            {
                var vm1 = new ViewModel1();
                var vm2 = new ViewModel2();

                var beforeRegisteredBindingsCount = PropertyBinding.RegisteredBindingsCount;
                
                PropertyBinding.Bind( () => vm1.PrimaryValue, () => vm2.SecondaryValue, BindingMode.TwoWay );

                Assert.That( PropertyBinding.RegisteredBindingsCount, Is.EqualTo( beforeRegisteredBindingsCount + 2 ) );
            }

            // allocate some memory and for GC and see whether bindings are removed from internal registration.
            {
                var a1 = new string[ 1000 * 1000 ];
                var a2 = new string[ 1000 * 1000 ];
                var a3 = new string[ 1000 * 1000 ];
                var a4 = new string[ 1000 * 1000 ];

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            {
                var vm1 = new ViewModel1();
                var vm2 = new ViewModel2();

                // this registration should now clean up the static references two the handlers
                PropertyBinding.Bind( () => vm1.PrimaryValue, () => vm2.SecondaryValue, BindingMode.TwoWay );

                Assert.That( PropertyBinding.RegisteredBindingsCount, Is.EqualTo( 2 ) );
            }
        }
    }
}

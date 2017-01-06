using System;
using System.Collections.Generic;
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
        public void PropertyOwnersStillAlive_BindingsSurvivesGC()
        {
            var vm1 = new ViewModel1();
            var vm2 = new ViewModel2();

            PropertyBinding.Bind( () => vm1.PrimaryValue, () => vm2.SecondaryValue, BindingMode.TwoWay );

            EnforceGC();

            {
                // lets see whether binding still works
                vm1.PrimaryValue = 42;
                Assert.That( vm2.SecondaryValue, Is.EqualTo( 42 ) );
            }
        }

        [TestCase]
        public void PropertyOwnersMarkedForGC_BindingReleased()
        {
            WeakReference<ViewModel1> wr1 = null;
            WeakReference<ViewModel2> wr2 = null;

            new Action( () =>
            {
                var vm1 = new ViewModel1();
                var vm2 = new ViewModel2();

                wr1 = new WeakReference<ViewModel1>( vm1 );
                wr2 = new WeakReference<ViewModel2>( vm2 );

                PropertyBinding.Bind( () => vm1.PrimaryValue, () => vm2.SecondaryValue, BindingMode.TwoWay );
            } )();

            EnforceGC();

            {
                ViewModel1 vm1;
                Assert.That( wr1.TryGetTarget( out vm1 ), Is.False );

                ViewModel2 vm2;
                Assert.That( wr2.TryGetTarget( out vm2 ), Is.False );
            }
        }

        private static volatile List<byte[]> x = new List<byte[]>();

        // allocate some memory and trigger GC 
        private static void EnforceGC()
        {
            for( int i = 0; i < 10000; ++i )
            {
                // do not go on LOH
                x.Add( new byte[ 10 * 1024 ] );
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}

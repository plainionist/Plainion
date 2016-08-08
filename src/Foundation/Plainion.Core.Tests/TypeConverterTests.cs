using System;
using System.Globalization;
using NUnit.Framework;

namespace Plainion.Core.Tests
{
    [TestFixture]
    class TypeConverterTests
    {
        [Test]
        public void ConvertTo_Double_Succeeds()
        {
            Assert.That( TypeConverter.ConvertTo( "4.5", typeof( double ) ), Is.EqualTo( 4.5d ) );
        }

        [Test]
        public void ConvertTo_NullableDouble_Succeeds()
        {
            Assert.That( TypeConverter.ConvertTo( "4.5", typeof( double? ) ), Is.EqualTo( ( double? )4.5d ) );
        }

        [Test]
        public void ConvertTo_Int_Succeeds()
        {
            Assert.That( TypeConverter.ConvertTo( "4", typeof( int ) ), Is.EqualTo( ( int )4 ) );
        }

        [Test]
        public void ConvertTo_NullableInt_Succeeds()
        {
            Assert.That( TypeConverter.ConvertTo( "4", typeof( int? ) ), Is.EqualTo( ( int? )4 ) );
        }

        [Test]
        public void ConvertTo_Long_Succeeds()
        {
            Assert.That( TypeConverter.ConvertTo( "4", typeof( long ) ), Is.EqualTo( ( long )4 ) );
        }

        [Test]
        public void ConvertTo_NullableLong_Succeeds()
        {
            Assert.That( TypeConverter.ConvertTo( "4", typeof( long? ) ), Is.EqualTo( ( long? )4 ) );
        }

        [Test]
        public void ConvertTo_Bool_Succeeds()
        {
            Assert.That( TypeConverter.ConvertTo( "true", typeof( bool ) ), Is.EqualTo( ( bool )true ) );
        }

        [Test]
        public void ConvertTo_NullableBool_Succeeds()
        {
            Assert.That( TypeConverter.ConvertTo( "true", typeof( bool? ) ), Is.EqualTo( ( bool? )true ) );
        }

        [Test]
        public void ConvertTo_DateTime_Succeeds()
        {
            Assert.That( TypeConverter.ConvertTo( "2012-12-12", typeof( DateTime ) ), Is.EqualTo( DateTime.ParseExact( "2012-12-12", "yyyy-MM-dd", CultureInfo.InvariantCulture ) ) );
        }

        [Test]
        public void ConvertTo_NullableDateTime_Succeeds()
        {
            Assert.That( TypeConverter.ConvertTo( "2012-12-12", typeof( DateTime? ) ), Is.EqualTo( (DateTime?)DateTime.ParseExact( "2012-12-12", "yyyy-MM-dd", CultureInfo.InvariantCulture ) ) );
        }
    }
}

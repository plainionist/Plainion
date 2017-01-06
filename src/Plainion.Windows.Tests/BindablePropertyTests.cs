using NUnit.Framework;
using Plainion.Windows.Tests.Fakes;

namespace Plainion.Windows.Tests
{
    [TestFixture]
    public class BindablePropertyTests : ViewModelBase
    {
        [TestCase]
        public void Create_InstanceAndPropertyName_OwnerAndPropertyCorrectlyDetected()
        {
            var vm = new ViewModel1();

            var prop = BindableProperty.Create( vm, "PrimaryValue" );

            Assert.That( prop.Owner, Is.EqualTo( vm ) );
            Assert.That( prop.PropertyName, Is.EqualTo( "PrimaryValue" ) );
        }

        [TestCase]
        public void Create_ExpressionWithLocalVariable_OwnerAndPropertyCorrectlyDetected()
        {
            var vm = new ViewModel1();

            var prop = BindableProperty.Create( () => vm.PrimaryValue );

            Assert.That( prop.Owner, Is.EqualTo( vm ) );
            Assert.That( prop.PropertyName, Is.EqualTo( "PrimaryValue" ) );
        }

        [TestCase]
        public void Create_ExpressionPath_OwnerAndPropertyCorrectlyDetected()
        {
            var vm = new ViewModel1();

            var prop = BindableProperty.Create( () => vm.Model.ModelValue );

            Assert.That( prop.Owner, Is.EqualTo( vm.Model ) );
            Assert.That( prop.PropertyName, Is.EqualTo( "ModelValue" ) );
        }

        public string Member { get; private set; }

        [TestCase]
        public void Create_ExpressionWithPropertyMember_OwnerAndPropertyCorrectlyDetected()
        {
            var prop = BindableProperty.Create( () => Member );

            Assert.That( prop.Owner, Is.EqualTo( this ) );
            Assert.That( prop.PropertyName, Is.EqualTo( "Member" ) );
        }

        internal Model Model { get; private set; }

        [TestCase]
        public void Create_ExpressionWithPropertyMemberPath_OwnerAndPropertyCorrectlyDetected()
        {
            Model = new Model();

            var prop = BindableProperty.Create( () => Model.ModelValue );

            Assert.That( prop.Owner, Is.EqualTo( Model ) );
            Assert.That( prop.PropertyName, Is.EqualTo( "ModelValue" ) );
        }

        private Model myModel;

        [TestCase]
        public void Create_ExpressionWithFieldMemberPath_OwnerAndPropertyCorrectlyDetected()
        {
            myModel = new Model();

            var prop = BindableProperty.Create( () => myModel.ModelValue );

            Assert.That( prop.Owner, Is.EqualTo( myModel ) );
            Assert.That( prop.PropertyName, Is.EqualTo( "ModelValue" ) );
        }
    }
}

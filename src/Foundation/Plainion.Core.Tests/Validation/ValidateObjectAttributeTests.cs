using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Plainion.Validation;
using System.ComponentModel.DataAnnotations;

namespace Plainion.UnitTests.Validation
{
    [TestFixture]
    public class ValidateObjectAttributeTests
    {
        /// <summary>
        /// A validation of properties of an object is not possible when the object itself is null.
        /// In this case - from perspective of the ValidateObjectAttribute - validation succeeded.
        /// </summary>
        [Test]
        public void GetValidationResult_WithNull_SuccessReturned()
        {
            var ctx = new ValidationContext( new object(), null, null );

            var attr = new ValidateObjectAttribute();
            var result = attr.GetValidationResult( null, ctx );

            Assert.That( result, Is.EqualTo( ValidationResult.Success ) );
        }

        [Test]
        public void GetValidationResult_ChildViolates_ViolationReturned()
        {
            var model = new Model();
            model.Name = "dummy";
            model.Node = new Node();
            model.Node.Description = null; // here we expect the exception

            var results = Validate( model );

            Assert.That( results.Count(), Is.EqualTo( 1 ) );
            Assert.That( results.Single(), Is.StringContaining( "The Description field is required." ) );
        }

        [Test]
        public void GetValidationResult_ItemInCollectionViolates_ViolationReturned()
        {
            var model = new Model();
            model.Name = "dummy";
            model.Node = null; // ignore
            model.Items = new List<Item> {
                new Item()
                { 
                    Value=1000 // here is the error
                } 
            };

            var results = Validate( model );

            Assert.That( results.Count(), Is.EqualTo( 1 ) );
            Assert.That( results.Single(), Is.StringContaining( "The field Value must be between 1 and 10." ) );
        }

        [Test]
        public void GetValidationResult_ItemInNestedCollectionViolates_ViolationReturned()
        {
            var model = new Model();
            model.Name = "dummy";
            model.Node = null; // ignore
            model.CollectionOfItems = new List<IEnumerable<Item>> {
                new List<Item>{
                    new Item()
                    { 
                        Value=1000 // here is the error
                    } 
                }
            };

            var results = Validate( model );

            Assert.That( results.Count(), Is.EqualTo( 1 ) );
            Assert.That( results.Single(), Is.StringContaining( "The field Value must be between 1 and 10." ) );
        }

        [Test]
        public void GetValidationResult_KeysInDictionary_ViolationReturned()
        {
            var dict = new Dictionary<Node, string>();
            dict.Add( new Node(), string.Empty );

            var results = Validate( dict );

            Assert.That( results.Count(), Is.EqualTo( 1 ) );
            Assert.That( results.Single(), Is.StringContaining( "The Description field is required." ) );
        }

        [Test]
        public void GetValidationResult_ValuesInDictionary_ViolationReturned()
        {
            var dict = new Dictionary<string, Node>();
            dict.Add( string.Empty, new Node() );

            var results = Validate( dict );

            Assert.That( results.Count(), Is.EqualTo( 1 ) );
            Assert.That( results.Single(), Is.StringContaining( "The Description field is required." ) );
        }

        private static IEnumerable<string> Validate( object model )
        {
            var ctx = new ValidationContext( model, null, null );

            var attr = new ValidateObjectAttribute();
            var result = attr.GetValidationResult( model, ctx );

            return RecursiveValidator.ExpandResults( result );
        }
    }
}

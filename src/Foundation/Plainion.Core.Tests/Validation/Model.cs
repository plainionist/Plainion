using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plainion.Validation;

namespace Plainion.UnitTests.Validation
{
    class Model
    {
        [Required]
        public string Name { get; set; }

        [ValidateObject]
        public Node Node { get; set; }

        [ValidateObject]
        public IEnumerable<Item> Items { get; set; }

        [ValidateObject]
        public IEnumerable<IEnumerable<Item>> CollectionOfItems { get; set; }
    }

    class Node
    {
        [Required]
        public string Description { get; set; }
    }

    public class Item
    {
        [Range( 1, 10 )]
        public int Value { get; set; }
    }
}

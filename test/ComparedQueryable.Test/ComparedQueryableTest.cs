using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ComparedQueryable.Test
{
    public class ComparedQueryableTest
    {
        [Fact]
        public void NaturallySortsIEnumerableStrings()
        {
            // GIVEN I have a collection with a string property
            var twoNorthStreet = new Address("2 N. Street");
            var tenNorthStreet = new Address("10 N. Street");
            var collection = new[] {tenNorthStreet, twoNorthStreet};

            // WHEN I order the collection by name
            var orderedCollection = collection
                .AsNaturalQueryable()
                .OrderBy(address => address.Line1);

            // THEN it should be ordered in "natural" order
            orderedCollection
                .ShouldBeEquivalentTo(new[] {twoNorthStreet, tenNorthStreet}, options => options.WithStrictOrdering(),
                    "our IQueryable provider should be able to naturally order strings");
        }

        [Fact]
        public void OrdersIntsAsNormal()
        {
            // GIVEN I have a collection with a string property
            var twoNorthStreet = new Address(2);
            var tenNorthStreet = new Address(10);
            var collection = new[] { tenNorthStreet, twoNorthStreet };

            // WHEN I order the collection by its numeric Id property
            var orderedCollection = collection
                .AsNaturalQueryable()
                .OrderBy(address => address.Id);

            // THEN it should be able to handle the ordering
            orderedCollection
                .ShouldBeEquivalentTo(new[] { twoNorthStreet, tenNorthStreet }, options => options.WithStrictOrdering(),
                    "our IQueryable provider should be able to normally order ints");
        }

        [Fact]
        public void HandlesOtherNonApplicableEnumerableMethods()
        {
            // GIVEN I have a collection with a string property
            var twoNorthStreet = new Address("2 N. Street");
            var tenNorthStreet = new Address("10 N. Street");
            var collection = new List<Address> { tenNorthStreet, twoNorthStreet };

            // WHEN I get the long count of the collection
            long longCount = 0;
            Action action = () => longCount = collection
                .AsNaturalQueryable()
                .LongCount();

            // THEN I should be able to get it
            action
                .ShouldNotThrow("we should be able to use IQueryable methods that don't have an IComparer argument");
            longCount
                .Should()
                .Be(2, "there are two elements in the collection");
        }

        [Fact]
        public void OrdersCollectionsByCustomComparers()
        {
            // GIVEN I have a collection with an int property
            var twoNorthStreet = new Address(2);
            var tenNorthStreet = new Address(10);
            var collection = new[] { twoNorthStreet, tenNorthStreet };

            // WHEN I order the collection by Id
            var orderedCollection = collection
                .AsComparedQueryable(Comparer<int>.Create((int1, int2) => int2.CompareTo(int1)))
                .OrderBy(address => address.Id);

            // THEN it should be ordered in my custom order
            orderedCollection
                .ShouldBeEquivalentTo(new[] { tenNorthStreet, twoNorthStreet },
                    options => options.WithStrictOrdering(),
                    "our IQueryable provider should be able to order anything using a custom comparer");
        }

        private class Address
        {
            public int Id { get; }
            public string Line1 { get; }

            public Address(string line1)
            {
                Line1 = line1;
            }

            public Address(int id)
            {
                Id = id;
            }
        }
    }
}

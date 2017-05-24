using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NaturalSort.Test
{
    public class NaturalSortTests
    {
        [Theory]
        [MemberData(nameof(UnsortedCollections))]
        public void NaturallySorts(IEnumerable<string> actualCollection, IEnumerable<string> expectedCollection)
        {
            // GIVEN I have a collection of strings

            // WHEN I try to order them naturally
            var orderedCollection = actualCollection
                .OrderBy(str => str, NaturalSortComparer.Instance)
                .ToList();

            // THEN I should expect the strings to be ordered in the way I expect
            orderedCollection
                .ShouldBeEquivalentTo(expectedCollection, options => options.WithStrictOrdering(),
                    "ordering naturally should make string collections be ordered the way a person would sort them");
        }

        public static object[] UnsortedCollections => new object[]
        {
            new object[]
            {
                new[] { "foo", "foobar" },
                new[] { "foo", "foobar" }
            },
            new object[]
            {
                new[] { "foobar", "foo" },
                new[] { "foo", "foobar" }
            },
            new object[]
            {
                new[] { "10", "2" },
                new[] { "2", "10" }
            },
            new object[]
            {
                new[] { "foo10", "foo2" },
                new[] { "foo2", "foo10" }
            },
            new object[]
            {
                new[] { "10foo2", "2foo2" },
                new[] { "2foo2", "10foo2" }
            },
            new object[]
            {
                new[] { "foo (10)", "foo (2)" },
                new[] { "foo (2)", "foo (10)" }
            },
            new object[]
            {
                new[] { "foo 10 bar 10 x", "foo 10 bar 2 x" },
                new[] { "foo 10 bar 2 x", "foo 10 bar 10 x" }
            },
            new object[]
            {
                new[] {"\"1\"", "\"JC\"", "1", "2", "21", "3"},
                new[] {"\"1\"", "\"JC\"", "1", "2", "3", "21"}
            }
        };
    }
}

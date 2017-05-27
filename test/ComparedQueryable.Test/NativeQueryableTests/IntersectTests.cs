// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ComparedQueryable.Test.NativeQueryableTests
{
    public class IntersectTests : EnumerableBasedTests
    {
        [Fact]
        public void BothEmpty()
        {
            int[] first = { };
            int[] second = { };
            Assert.Empty(first.AsNaturalQueryable().Intersect(second.AsNaturalQueryable()));
        }

        [Fact]
        public void FirstNullCustomComparer()
        {
            IQueryable<string> first = null;
            string[] second = { "ekiM", "bBo" };

            var ane = AssertExtensions.Throws<ArgumentNullException>("source1", () => first.Intersect(second.AsNaturalQueryable(), new AnagramEqualityComparer()));
        }

        [Fact]
        public void SecondNullCustomComparer()
        {
            string[] first = { "Tim", "Bob", "Mike", "Robert" };
            IQueryable<string> second = null;

            var ane = AssertExtensions.Throws<ArgumentNullException>("source2", () => first.AsNaturalQueryable().Intersect(second, new AnagramEqualityComparer()));
        }

        [Fact]
        public void FirstNullNoComparer()
        {
            IQueryable<string> first = null;
            string[] second = { "ekiM", "bBo" };

            var ane = AssertExtensions.Throws<ArgumentNullException>("source1", () => first.Intersect(second.AsNaturalQueryable()));
        }

        [Fact]
        public void SecondNullNoComparer()
        {
            string[] first = { "Tim", "Bob", "Mike", "Robert" };
            IQueryable<string> second = null;

            var ane = AssertExtensions.Throws<ArgumentNullException>("source2", () => first.AsNaturalQueryable().Intersect(second));
        }

        [Fact]
        public void SingleNullWithEmpty()
        {
            string[] first = { null };
            string[] second = new string[0];
            Assert.Empty(first.AsNaturalQueryable().Intersect(second.AsNaturalQueryable(), EqualityComparer<string>.Default));
        }

        [Fact]
        public void CustomComparer()
        {
            string[] first = { "Tim", "Bob", "Mike", "Robert" };
            string[] second = { "ekiM", "bBo" };
            string[] expected = { "Bob", "Mike" };

            Assert.Equal(expected, first.AsNaturalQueryable().Intersect(second.AsNaturalQueryable(), new AnagramEqualityComparer()));
        }

        [Fact]
        public void Intersect1()
        {
            var count = (new int[] { 0, 1, 2 }).AsNaturalQueryable().Intersect((new int[] { 1, 2, 3 }).AsNaturalQueryable()).Count();
            Assert.Equal(2, count);
        }

        [Fact]
        public void Intersect2()
        {
            var count = (new int[] { 0, 1, 2 }).AsNaturalQueryable().Intersect((new int[] { 1, 2, 3 }).AsNaturalQueryable(), EqualityComparer<int>.Default).Count();
            Assert.Equal(2, count);
        }
    }
}

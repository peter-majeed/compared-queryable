// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ComparedQueryable.Test.NativeQueryableTests
{
    public class UnionTests : EnumerableBasedTests
    {        
        [Fact]
        public void CustomComparer()
        {
            string[] first = { "Bob", "Robert", "Tim", "Matt", "miT" };
            string[] second = { "ttaM", "Charlie", "Bbo" };
            string[] expected = { "Bob", "Robert", "Tim", "Matt", "Charlie" };
            
            var comparer = new AnagramEqualityComparer();

            Assert.Equal(expected, first.AsNaturalQueryable().Union(second.AsNaturalQueryable(), comparer), comparer);
        }

        [Fact]
        public void FirstNullCustomComparer()
        {
            IQueryable<string> first = null;
            string[] second = { "ttaM", "Charlie", "Bbo" };
            
            var ane = AssertExtensions.Throws<ArgumentNullException>("source1", () => first.Union(second.AsNaturalQueryable(), new AnagramEqualityComparer()));
        }

        [Fact]
        public void SecondNullCustomComparer()
        {
            string[] first = { "Bob", "Robert", "Tim", "Matt", "miT" };
            IQueryable<string> second = null;

            var ane = AssertExtensions.Throws<ArgumentNullException>("source2", () => first.AsNaturalQueryable().Union(second, new AnagramEqualityComparer()));
        }

        [Fact]
        public void FirstNullNoComparer()
        {
            IQueryable<string> first = null;
            string[] second = { "ttaM", "Charlie", "Bbo" };

            var ane = AssertExtensions.Throws<ArgumentNullException>("source1", () => first.Union(second.AsNaturalQueryable()));
        }

        [Fact]
        public void SecondNullNoComparer()
        {
            string[] first = { "Bob", "Robert", "Tim", "Matt", "miT" };
            IQueryable<string> second = null;

            var ane = AssertExtensions.Throws<ArgumentNullException>("source2", () => first.AsNaturalQueryable().Union(second));
        }

        [Fact]
        public void CommonElementsShared()
        {
            int[] first = { 1, 2, 3, 4, 5, 6 };
            int[] second = { 6, 7, 7, 7, 8, 1 };
            int[] expected = { 1, 2, 3, 4, 5, 6, 7, 8 };

            Assert.Equal(expected, first.AsNaturalQueryable().Union(second.AsNaturalQueryable()));
        }

        [Fact]
        public void Union1()
        {
            var count = (new int[] { 0, 1, 2 }).AsNaturalQueryable().Union((new int[] { 1, 2, 3 }).AsNaturalQueryable()).Count();
            Assert.Equal(4, count);
        }

        [Fact]
        public void Union2()
        {
            var count = (new int[] { 0, 1, 2 }).AsNaturalQueryable().Union((new int[] { 1, 2, 3 }).AsNaturalQueryable(), EqualityComparer<int>.Default).Count();
            Assert.Equal(4, count);
        }
    }
}

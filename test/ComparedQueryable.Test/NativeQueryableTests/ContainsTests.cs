// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ComparedQueryable.Test.NativeQueryableTests
{
    public class ContainsTests : EnumerableBasedTests
    {
        [Fact]
        public void Empty()
        {
            int[] source = { };

            Assert.False(source.AsNaturalQueryable().Contains(6));
        }

        [Fact]
        public void NotPresent()
        {
            int[] source = { 8, 10, 3, 0, -8 };
            
            Assert.False(source.AsNaturalQueryable().Contains(6));
        }

        [Fact]
        public void MultipleMatches()
        {
            int[] source = { 8, 0, 10, 3, 0, -8, 0 };
            
            Assert.True(source.AsNaturalQueryable().Contains(0));
        }

        [Fact]
        public void DefaultComparerFromNull()
        {
            string[] source = { "Bob", "Robert", "Tim" };

            Assert.False(source.AsNaturalQueryable().Contains("trboeR", null));
            Assert.True(source.AsNaturalQueryable().Contains("Tim", null));
        }

        [Fact]
        public void CustomComparerFromNull()
        {
            string[] source = { "Bob", "Robert", "Tim" };
            
            Assert.True(source.AsNaturalQueryable().Contains("trboeR", new AnagramEqualityComparer()));
            Assert.False(source.AsNaturalQueryable().Contains("nevar", new AnagramEqualityComparer()));
        }
        
        [Fact]
        public void NullSource()
        {
            IQueryable<int> source = null;
            
            AssertExtensions.Throws<ArgumentNullException>("source", () => source.Contains(42));
            AssertExtensions.Throws<ArgumentNullException>("source", () => source.Contains(42, EqualityComparer<int>.Default));
        }

        [Fact]
        public void Contains1()
        {
            var val = (new int[] { 0, 2, 1 }).AsNaturalQueryable().Contains(1);
            Assert.True(val);
        }

        [Fact]
        public void Contains2()
        {
            var val = (new int[] { 0, 2, 1 }).AsNaturalQueryable().Contains(1, EqualityComparer<int>.Default);
            Assert.True(val);
        }
    }
}

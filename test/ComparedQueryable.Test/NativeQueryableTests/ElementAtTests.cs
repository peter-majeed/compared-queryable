// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Xunit;

namespace ComparedQueryable.Test.NativeQueryableTests
{
    public class ElementAtTests : EnumerableBasedTests
    {
        [Fact]
        public void IndexNegative()
        {
            int?[] source = { 9, 8 };
            
            AssertExtensions.Throws<ArgumentOutOfRangeException>("index", () => source.AsNaturalQueryable().ElementAt(-1));
        }

        [Fact]
        public void IndexEqualsCount()
        {
            int[] source = { 1, 2, 3, 4 };
            
            AssertExtensions.Throws<ArgumentOutOfRangeException>("index", () => source.AsNaturalQueryable().ElementAt(source.Length));
        }

        [Fact]
        public void EmptyIndexZero()
        {
            int[] source = { };
            
            AssertExtensions.Throws<ArgumentOutOfRangeException>("index", () => source.AsNaturalQueryable().ElementAt(0));
        }

        [Fact]
        public void SingleElementIndexZero()
        {
            int[] source = { -4 };
            
            Assert.Equal(-4, source.AsNaturalQueryable().ElementAt(0));
        }

        [Fact]
        public void ManyElementsIndexTargetsLast()
        {
            int[] source = { 9, 8, 0, -5, 10 };
            
            Assert.Equal(10, source.AsNaturalQueryable().ElementAt(source.Length - 1));
        }

        [Fact]
        public void NullSource()
        {
            AssertExtensions.Throws<ArgumentNullException>("source", () => ((IQueryable<int>)null).ElementAt(2));
        }

        [Fact]
        public void ElementAt()
        {
            var val = (new int[] { 0, 2, 1 }).AsNaturalQueryable().ElementAt(1);
            Assert.Equal(2, val);
        }
    }
}

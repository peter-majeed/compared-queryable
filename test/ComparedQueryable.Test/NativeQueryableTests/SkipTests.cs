// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Xunit;

namespace ComparedQueryable.Test.NativeQueryableTests
{
    public class SkipTests : EnumerableBasedTests
    {
        [Fact]
        public void SkipSome()
        {
            Assert.Equal(Enumerable.Range(10, 10).AsNaturalQueryable(), Enumerable.Range(0, 20).AsNaturalQueryable().Skip(10));
        }

        [Fact]
        public void SkipExcessive()
        {
            Assert.Empty(Enumerable.Range(0, 20).AsNaturalQueryable().Skip(42));
        }

        [Fact]
        public void SkipThrowsOnNull()
        {
            AssertExtensions.Throws<ArgumentNullException>("source", () => ((IQueryable<DateTime>)null).Skip(3));
        }

        [Fact]
        public void Skip()
        {
            var count = (new int[] { 0, 1, 2 }).AsNaturalQueryable().Skip(1).Count();
            Assert.Equal(2, count);
        }
    }
}

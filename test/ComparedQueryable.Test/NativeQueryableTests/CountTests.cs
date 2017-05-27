// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace ComparedQueryable.Test.NativeQueryableTests
{
    public class CountTests : EnumerableBasedTests
    {
        [Fact]
        public void Empty()
        {
            Assert.Equal(0, Enumerable.Empty<int>().AsNaturalQueryable().Count());
        }

        [Fact]
        public void EmptySourceWithPredicate()
        {
            Assert.Equal(0, Enumerable.Empty<int>().AsNaturalQueryable().Count(i => i % 2 == 0));
        }

        [Fact]
        public void NonEmpty()
        {
            int?[] data = { -10, 4, 9, null, 11 };
            Assert.Equal(5, data.AsNaturalQueryable().Count());
        }

        [Fact]
        public void PredicateTrueFirstAndLast()
        {
            int[] data = { 2, 5, 7, 9, 29, 10 };
            Assert.Equal(2, data.AsNaturalQueryable().Count(i => i % 2 == 0));
        }

        [Fact]
        public void NullSource()
        {
            AssertExtensions.Throws<ArgumentNullException>("source", () => ((IQueryable<int>)null).Count());
        }

        [Fact]
        public void NullSourcePredicateUsed()
        {
            AssertExtensions.Throws<ArgumentNullException>("source", () => ((IQueryable<int>)null).Count(i => i != 0));
        }

        [Fact]
        public void NullPredicateUsed()
        {
            Expression<Func<int, bool>> predicate = null;
            AssertExtensions.Throws<ArgumentNullException>("predicate", () => Enumerable.Range(0, 3).AsNaturalQueryable().Count(predicate));
        }

        [Fact]
        public void Count1()
        {
            var count = (new int[] { 0 }).AsNaturalQueryable().Count();
            Assert.Equal(1, count);
        }

        [Fact]
        public void Count2()
        {
            var count = (new int[] { 0, 1, 2 }).AsNaturalQueryable().Count(n => n > 0);
            Assert.Equal(2, count);
        }
    }
}

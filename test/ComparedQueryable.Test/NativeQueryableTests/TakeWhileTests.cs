// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace ComparedQueryable.Test.NativeQueryableTests
{
    public class TakeWhileTests : EnumerableBasedTests
    {
        [Fact]
        public void SourceNonEmptyPredicateTrueSomeFalseSecond()
        {
            int[] source = { 8, 3, 12, 4, 6, 10 };
            int[] expected = { 8 };

            Assert.Equal(expected, source.AsNaturalQueryable().TakeWhile(x => x % 2 == 0));
        }

        [Fact]
        public void SourceNonEmptyPredicateTrueSomeFalseSecondWithIndex()
        {
            int[] source = { 8, 3, 12, 4, 6, 10 };
            int[] expected = { 8 };

            Assert.Equal(expected, source.AsNaturalQueryable().TakeWhile((x, i) => x % 2 == 0));
        }

        [Fact]
        public void ThrowsOnNullSource()
        {
            IQueryable<int> source = null;
            AssertExtensions.Throws<ArgumentNullException>("source", () => source.TakeWhile(x => true));
        }

        [Fact]
        public void ThrowsOnNullPredicate()
        {
            IQueryable<int> source = new[] { 1, 2, 3 }.AsNaturalQueryable();
            Expression<Func<int, bool>> nullPredicate = null;
            AssertExtensions.Throws<ArgumentNullException>("predicate", () => source.TakeWhile(nullPredicate));
        }

        [Fact]
        public void ThrowsOnNullSourceIndexed()
        {
            IQueryable<int> source = null;
            AssertExtensions.Throws<ArgumentNullException>("source", () => source.TakeWhile((x, i) => true));
        }

        [Fact]
        public void ThrowsOnNullPredicateIndexed()
        {
            IQueryable<int> source = new[] { 1, 2, 3 }.AsNaturalQueryable();
            Expression<Func<int, int, bool>> nullPredicate = null;
            AssertExtensions.Throws<ArgumentNullException>("predicate", () => source.TakeWhile(nullPredicate));
        }

        [Fact]
        public void TakeWhile1()
        {
            var count = (new int[] { 0, 1, 2 }).AsNaturalQueryable().TakeWhile(n => n < 2).Count();
            Assert.Equal(2, count);
        }

        [Fact]
        public void TakeWhile2()
        {
            var count = (new int[] { 0, 1, 2 }).AsNaturalQueryable().TakeWhile((n, i) => n + i < 4).Count();
            Assert.Equal(2, count);
        }
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace ComparedQueryable.Test.NativeQueryableTests
{
    public class AllTests : EnumerableBasedTests
    {
        [Fact]
        public void PredicateTrueAllExceptLast()
        {
            int[] source = { 4, 2, 10, 12, 8, 6, 3 };

            Assert.False(source.AsNaturalQueryable().All(i => i % 2 == 0));
        }

        [Fact]
        public void NullSource()
        {
            AssertExtensions.Throws<ArgumentNullException>("source", () => ((IQueryable<int>)null).All(i => i != 0));
        }

        [Fact]
        public void NullPredicateUsed()
        {
            Expression<Func<int, bool>> predicate = null;
            AssertExtensions.Throws<ArgumentNullException>("predicate", () => Enumerable.Range(0, 3).AsNaturalQueryable().All(predicate));
        }

        [Fact]
        public void All()
        {
            var val = (new int[] { 0, 2, 1 }).AsNaturalQueryable().All(n => n > 1);
            Assert.False(val);
        }
    }
}

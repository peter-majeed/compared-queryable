// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Xunit;

namespace ComparedQueryable.Test.NativeQueryableTests
{
    public class OfTypeTests : EnumerableBasedTests
    {
        [Fact]
        public void EmptySource()
        {
            object[] source = { };
            Assert.Empty(source.AsNaturalQueryable().OfType<int>());
        }

        [Fact]
        public void HeterogenousSourceOnlyFirstOfType()
        {
            object[] source = { 10, "Hello", 3.5, "Test" };
            int[] expected = { 10 };

            Assert.Equal(expected, source.AsNaturalQueryable().OfType<int>());
        }

        [Fact]
        public void NullSource()
        {
            AssertExtensions.Throws<ArgumentNullException>("source", () => ((IQueryable<object>)null).OfType<string>());
        }

        [Fact]
        public void OfType()
        {
            var count = (new object[] { 0, (long)1, 2 }).AsNaturalQueryable().OfType<int>().Count();
            Assert.Equal(2, count);
        }
    }
}

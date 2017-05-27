// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Xunit;

namespace ComparedQueryable.Test.NativeQueryableTests
{
    public class DefaultIfEmptyTests : EnumerableBasedTests
    {
        [Fact]
        public void EmptyNullableSourceNoDefaultPassed()
        {
            int?[] source = { };
            int?[] expected = { default(int?) };

            Assert.Equal(expected, source.AsNaturalQueryable().DefaultIfEmpty());
        }

        [Fact]
        public void EmptyNonNullableSourceNoDefaultPassed()
        {
            int[] source = { };
            int[] expected = { default(int) };

            Assert.Equal(expected, source.AsNaturalQueryable().DefaultIfEmpty());
        }

        [Fact]
        public void SeveralElementsNoDefaultPassed()
        {
            int[] source = { 3, -1, 0, 10, 15 };

            Assert.Equal(source, source.AsNaturalQueryable().DefaultIfEmpty());
        }

        [Fact]
        public void EmptyNullableDefaultValuePassed()
        {
            int?[] source = { };
            int? defaultValue = 9;
            int?[] expected = { defaultValue };

            Assert.Equal(expected, source.AsNaturalQueryable().DefaultIfEmpty(defaultValue));
        }

        [Fact]
        public void EmptyNonNullableDefaultValuePassed()
        {
            int[] source = { };
            int defaultValue = -10;
            int[] expected = { defaultValue };

            Assert.Equal(expected, source.AsNaturalQueryable().DefaultIfEmpty(defaultValue));
        }

        [Fact]
        public void NullSource()
        {
            IQueryable<int> source = null;
            
            AssertExtensions.Throws<ArgumentNullException>("source", () => source.DefaultIfEmpty());
            AssertExtensions.Throws<ArgumentNullException>("source", () => source.DefaultIfEmpty(42));
        }

        [Fact]
        public void DefaultIfEmpty1()
        {
            var count = (new int[] { }).AsNaturalQueryable().DefaultIfEmpty().Count();
            Assert.Equal(1, count);
        }

        [Fact]
        public void DefaultIfEmpty2()
        {
            var count = (new int[] { }).AsNaturalQueryable().DefaultIfEmpty(3).Count();
            Assert.Equal(1, count);
        }

    }
}

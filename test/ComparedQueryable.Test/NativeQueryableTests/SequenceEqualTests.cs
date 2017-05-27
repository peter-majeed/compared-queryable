// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ComparedQueryable.Test.NativeQueryableTests
{
    public class SequenceEqualTests : EnumerableBasedTests
    {

        [Fact]
        public void MismatchInMiddle()
        {
            int?[] first = { 1, 2, 3, 4 };
            int?[] second = { 1, 2, 6, 4 };

            Assert.False(first.AsNaturalQueryable().SequenceEqual(second.AsNaturalQueryable()));
        }

        [Fact]
        public void NullComparer()
        {
            string[] first = { "Bob", "Tim", "Chris" };
            string[] second = { "Bbo", "mTi", "rishC" };

            Assert.False(first.AsNaturalQueryable().SequenceEqual(second.AsNaturalQueryable()));
        }

        [Fact]
        public void CustomComparer()
        {
            string[] first = { "Bob", "Tim", "Chris" };
            string[] second = { "Bbo", "mTi", "rishC" };

            Assert.True(first.AsNaturalQueryable().SequenceEqual(second.AsNaturalQueryable(), new AnagramEqualityComparer()));
        }

        [Fact]
        public void FirstSourceNull()
        {
            IQueryable<int> first = null;
            int[] second = { };
            AssertExtensions.Throws<ArgumentNullException>("source1", () => first.SequenceEqual(second.AsNaturalQueryable()));
            AssertExtensions.Throws<ArgumentNullException>("source1", () => first.SequenceEqual(second.AsNaturalQueryable(), null));
        }

        [Fact]
        public void SecondSourceNull()
        {
            int[] first = { };
            IQueryable<int> second = null;
            AssertExtensions.Throws<ArgumentNullException>("source2", () => first.AsNaturalQueryable().SequenceEqual(second));
            AssertExtensions.Throws<ArgumentNullException>("source2", () => first.AsNaturalQueryable().SequenceEqual(second, null));
        }

        [Fact]
        public void SequenceEqual1()
        {
            var val = (new int[] { 0, 2, 1 }).AsNaturalQueryable().SequenceEqual(new int[] { 0, 2, 1 });
            Assert.True(val);
        }

        [Fact]
        public void SequenceEqual2()
        {
            var val = (new int[] { 0, 2, 1 }).AsNaturalQueryable().SequenceEqual(new int[] { 0, 2, 1 }, EqualityComparer<int>.Default);
            Assert.True(val);
        }
    }
}

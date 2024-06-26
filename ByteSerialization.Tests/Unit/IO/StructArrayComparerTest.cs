﻿// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using Xunit;

namespace ByteSerialization.Tests.Unit.IO
{
    public class StructArrayComparerTest
    {
        [Fact]
        public void Test_Simple()
        {
            bool[] left = { true, true, false };
            bool[] right = { true, true, true };

            var differences = StructArrayComparer.Compare(left, right);
            Assert.Single(differences);
            AssertDifference(differences.Single(), 2, false, true);
        }

        [Fact]
        public void Test_DifferentLengths()
        {
            bool[] left = { true, };
            bool[] right = { true, false, true };

            var differences = StructArrayComparer.Compare(left, right);
            AssertDifference(differences[0], 1, null, false);
            AssertDifference(differences[1], 2, null, true);
        }

        private void AssertDifference<TItem>(
            StructArrayComparer.Difference<TItem> difference, int index, TItem? expectedLeft, TItem? expectedRight)
            where TItem : struct
        {
            Assert.Equal(expectedLeft, difference.Left);
            Assert.Equal(expectedRight, difference.Right);
        }
    }
}

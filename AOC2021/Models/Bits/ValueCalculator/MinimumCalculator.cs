// -----------------------------------------------------------------------
// <copyright file="MinimumCalculator.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits.ValueCalculator
{
    using FluentAssertions;

    internal class MinimumCalculator : IValueCalculator
    {
        public long Calculate(List<long> childValues)
        {
            childValues.Count.Should().BeGreaterThanOrEqualTo(1);

            long result = childValues.Min();
            return result;
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="MaximumCalculator.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits.ValueCalculator
{
    using FluentAssertions;

    internal class MaximumCalculator : IValueCalculator
    {
        public long Calculate(List<long> childValues)
        {
            childValues.Count.Should().BeGreaterThanOrEqualTo(1);

            long result = childValues.Max();
            return result;
        }
    }
}

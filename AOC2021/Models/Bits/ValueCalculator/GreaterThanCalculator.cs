// -----------------------------------------------------------------------
// <copyright file="GreaterThanCalculator.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits.ValueCalculator
{
    using FluentAssertions;

    internal class GreaterThanCalculator : IValueCalculator
    {
        public long Calculate(List<long> childValues)
        {
            childValues.Count.Should().Be(2);

            long result = childValues[0] > childValues[1] ? 1L : 0;
            return result;
        }
    }
}

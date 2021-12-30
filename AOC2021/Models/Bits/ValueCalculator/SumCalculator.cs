// -----------------------------------------------------------------------
// <copyright file="SumCalculator.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits.ValueCalculator
{
    using FluentAssertions;

    internal class SumCalculator : IValueCalculator
    {
        public long Calculate(List<long> childValues)
        {
            childValues.Count.Should().BeGreaterThanOrEqualTo(1);

            long sum = 0;
            foreach (long item in childValues)
            {
                checked
                {
                    sum += item;
                }
            }

            return sum;
        }
    }
}

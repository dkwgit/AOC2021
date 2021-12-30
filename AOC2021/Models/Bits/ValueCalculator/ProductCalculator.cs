// -----------------------------------------------------------------------
// <copyright file="ProductCalculator.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits.ValueCalculator
{
    using FluentAssertions;

    internal class ProductCalculator : IValueCalculator
    {
        public long Calculate(List<long> childValues)
        {
            childValues.Count.Should().BeGreaterThanOrEqualTo(1);

            long result = 1;
            foreach (long item in childValues)
            {
                checked
                {
                    result *= item;
                }
            }

            return result;
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="IValueCalculator.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits.ValueCalculator
{
    internal interface IValueCalculator
    {
        long Calculate(List<long> childValues);
    }
}

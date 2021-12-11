// -----------------------------------------------------------------------
// <copyright file="IDayResult.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    public interface IDayResult
    {
        string DayName { get; }

        int ResultNumber { get; }

        string ResultDescription { get; }

        long Result { get; }

        string Type { get; }
    }
}
// -----------------------------------------------------------------------
// <copyright file="DayResult.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    public record DayResult(string DayName, int ResultNumber, string ResultDescription, long Result, string Type = "Standard") : IDayResult;
}

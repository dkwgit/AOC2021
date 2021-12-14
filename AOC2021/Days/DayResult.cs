// -----------------------------------------------------------------------
// <copyright file="DayResult.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    public record DayResult(string DayName, int ResultNumber, string ResultDescription, string Result, long ExecutionTime, string Type = "Standard") : IDayResult;
}

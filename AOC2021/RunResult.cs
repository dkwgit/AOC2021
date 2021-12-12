// -----------------------------------------------------------------------
// <copyright file="RunResult.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
    using AOC2021.Days;

    public record RunResult(string DayName, string DayDescription, IDayResult[] DayResults, long ExecutionTime);
}

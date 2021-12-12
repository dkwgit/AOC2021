// -----------------------------------------------------------------------
// <copyright file="IRunResult.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Run
{
    using AOC2021.Days;

    public interface IRunResult
    {
        string DayDescription { get; }

        string DayName { get; }

        IDayResult[] DayResults { get; }

        long ExecutionTime { get; }
    }
}
// -----------------------------------------------------------------------
// <copyright file="DayRunner.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
    using System.Diagnostics;

    /// <summary>
    /// Runs a day an returns its results.
    /// </summary>
    public class DayRunner
    {
        internal RunResult RunDay(IDay day)
        {
            Stopwatch sw = new();
            sw.Start();

            int result1 = day.Result1();
            int result2 = day.Result2();

            sw.Stop();

            return new RunResult(day.Name, result1, result2, sw.ElapsedMilliseconds);
        }
    }
}

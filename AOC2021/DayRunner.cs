// -----------------------------------------------------------------------
// <copyright file="DayRunner.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
    using System.Diagnostics;
    using AOC2021.Days;

    /// <summary>
    /// Runs a day an returns its results.
    /// </summary>
    public class DayRunner
    {
        internal List<RunResult> RunDays(IEnumerable<IDay> days)
        {
            List<RunResult> results = new();
            foreach (var day in days)
            {
                results.Add(this.RunDay(day));
            }

            return results;
        }

        internal RunResult RunDay(IDay day)
        {
            Stopwatch sw = new();
            sw.Start();

            IDayResult[] results = day.GetResults();

            sw.Stop();

            return new RunResult(day.GetName(), day.GetDescription(), results, sw.ElapsedMilliseconds);
        }
    }
}

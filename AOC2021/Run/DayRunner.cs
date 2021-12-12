// -----------------------------------------------------------------------
// <copyright file="DayRunner.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Run
{
    using System.Diagnostics;
    using AOC2021.Days;

    /// <summary>
    /// Runs a day an returns its results.
    /// </summary>
    public class DayRunner
    {
        internal int ExecutionCount { get; set; } = 1;

        internal List<IRunResult> RunDays(IEnumerable<IDay> days)
        {
            List<IRunResult> results = new();
            foreach (var day in days)
            {
                if (this.ExecutionCount == 1)
                {
                    results.Add(this.RunDay(day));
                }
                else
                {
                    results.Add(this.RunDay(day, this.ExecutionCount));
                }
            }

            return results;
        }

        internal IRunResult RunDay(IDay day)
        {
            Stopwatch sw = new();
            sw.Start();

            IDayResult[] results = day.GetResults();

            sw.Stop();

            return new RunResult(day.GetName(), day.GetDescription(), results, sw.ElapsedMilliseconds);
        }

        internal IRunResult RunDay(IDay day, int executionCount)
        {
            AggregateRunResult runResult = new(day.GetName(), day.GetDescription(), executionCount);

            Stopwatch sw = new();
            sw.Start();

            for (int i = 0; i < executionCount; i++)
            {
                IDayResult[] results = day.GetResults();
                runResult.AggregateResults.Add(results);
            }

            sw.Stop();
            runResult.ExecutionTime = sw.ElapsedMilliseconds;

            return runResult;
        }
    }
}

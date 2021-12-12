// -----------------------------------------------------------------------
// <copyright file="AggregateRunResult.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Run
{
    using AOC2021.Days;

    internal class AggregateRunResult : IRunResult
    {
        private long executionTime;

        internal AggregateRunResult(string dayDescription, string dayName, int executionCount)
        {
            this.DayDescription = dayDescription;
            this.DayName = dayName;
            this.ExecutionCount = executionCount;
        }

        public string DayDescription { get; }

        public string DayName { get; }

        public IDayResult[] DayResults
        {
            get
            {
                return (from resultItems in this.AggregateResults
                from result in resultItems
                group result by new { result.DayName, result.ResultNumber, result.ResultDescription, result.Result, result.Type } into grp
                select new DayResult(grp.Key.DayName, grp.Key.ResultNumber, grp.Key.ResultDescription, grp.Key.Result, grp.Sum(r => r.ExecutionTime) / this.ExecutionCount, grp.Key.Type)).ToArray();
            }
        }

        public long ExecutionTime
        {
            get
            {
                return this.executionTime / this.ExecutionCount;
            }

            set
            {
                this.executionTime = value;
            }
        }

        internal List<IDayResult[]> AggregateResults { get; } = new();

        internal int ExecutionCount { get; }
    }
}

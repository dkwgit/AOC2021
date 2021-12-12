// -----------------------------------------------------------------------
// <copyright file="BaseDay.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Diagnostics;

    public abstract class BaseDay : IDay
    {
        public string GetName()
        {
            return this.GetType().Name;
        }

        public abstract string GetDescription();

        public abstract long Result1();

        public abstract long Result2();

        public virtual IDayResult[] GetResults()
        {
            Stopwatch sw = new();
            sw.Start();
            long result1 = this.Result1();
            sw.Stop();
            long timing1 = sw.ElapsedMilliseconds;

            sw.Restart();
            long result2 = this.Result2();
            sw.Stop();
            long timing2 = sw.ElapsedMilliseconds;

            return new IDayResult[]
            {
                new DayResult(this.GetName(), 1, string.Empty, result1, timing1),
                new DayResult(this.GetName(), 2, string.Empty, result2, timing2),
            };
        }
    }
}
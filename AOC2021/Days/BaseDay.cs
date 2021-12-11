// -----------------------------------------------------------------------
// <copyright file="BaseDay.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    public abstract class BaseDay : IDay
    {
        public string GetName()
        {
            return this.GetType().Name;
        }

        public abstract long Result1();

        public abstract long Result2();

        public virtual IDayResult[] GetResults()
        {
            return new IDayResult[]
            {
                new DayResult(this.GetName(), 1, string.Empty, this.Result1()),
                new DayResult(this.GetName(), 2, string.Empty, this.Result2()),
            };
        }
    }
}
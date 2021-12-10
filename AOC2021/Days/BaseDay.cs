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
    }
}
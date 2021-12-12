﻿// -----------------------------------------------------------------------
// <copyright file="IDay.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    internal interface IDay
    {
        internal string GetName();

        internal long Result1();

        internal long Result2();

        internal IDayResult[] GetResults();

        internal string GetDescription();
    }
}

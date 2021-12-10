// -----------------------------------------------------------------------
// <copyright file="IDay.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal interface IDay
    {
        internal string GetName();

        internal long Result1();

        internal long Result2();
    }
}

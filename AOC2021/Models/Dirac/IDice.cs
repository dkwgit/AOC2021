// -----------------------------------------------------------------------
// <copyright file="IDice.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dirac
{
    internal interface IDice
    {
        int RollCount { get; set; }

        int RollDice();
    }
}

// -----------------------------------------------------------------------
// <copyright file="IPair.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.SnailFish
{
    internal interface IPair
    {
        IPairItem Left { get; set; }

        IPairItem Right { get; set; }

        int GetMagnitude();
    }
}

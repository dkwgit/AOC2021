// -----------------------------------------------------------------------
// <copyright file="PairItem.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.SnailFish
{
    internal abstract class PairItem : IPairItem
    {
        public abstract int GetMagnitude();
    }
}

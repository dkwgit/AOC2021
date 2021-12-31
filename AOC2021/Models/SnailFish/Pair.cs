// -----------------------------------------------------------------------
// <copyright file="Pair.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.SnailFish
{
    internal class Pair : PairItem, IPair, IPairItem
    {
        internal Pair(IPairItem leftItem, IPairItem rightItem)
        {
            this.Left = leftItem;
            this.Right = rightItem;
        }

        public IPairItem Left { get; set; }

        public IPairItem Right { get; set; }

        public override int GetMagnitude()
        {
            return (3 * this.Left.GetMagnitude()) + (2 * this.Right.GetMagnitude());
        }
    }
}

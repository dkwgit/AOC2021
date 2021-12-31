// -----------------------------------------------------------------------
// <copyright file="UnresolvedPairItem.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.SnailFish
{
    internal class UnresolvedPairItem : PairItem, IPairItem
    {
        public int Counter { get; set; } = 0;

        public UnresolvedPairItem Increment()
        {
            this.Counter++;
            return this;
        }

        public UnresolvedPairItem Decrement()
        {
            this.Counter--;
            return this;
        }

        public override int GetMagnitude()
        {
            throw new NotImplementedException();
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="ValueItem.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.SnailFish
{
    internal class ValueItem : PairItem
    {
        internal ValueItem(int value)
        {
            this.Value = value;
        }

        public int Value { get; }

        public override int GetMagnitude()
        {
            return this.Value;
        }
    }
}

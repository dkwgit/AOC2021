// -----------------------------------------------------------------------
// <copyright file="AbstractDice.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dirac
{
    internal abstract class AbstractDice : IDice
    {
        public int RollCount { get; set; } = 0;

        public int RollDice()
        {
            this.RollCount++;

            return this.GetRollValue();
        }

        protected abstract int GetRollValue();
    }
}

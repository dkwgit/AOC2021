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

        public int RollDice(int times, int playerIndex)
        {
            int sum = 0;
            for (int t = 0; t < times; t++)
            {
                sum += this.GetRollValue(playerIndex);
                this.RollCount++;
            }

            return sum;
        }

        protected abstract int GetRollValue(int playerIndex);
    }
}

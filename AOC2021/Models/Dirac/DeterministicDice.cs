// -----------------------------------------------------------------------
// <copyright file="DeterministicDice.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dirac
{
    internal class DeterministicDice : AbstractDice
    {
        private int NextDiceValue { get; set; } = 1;

        protected override int GetRollValue(int playerIndex /* ignored */)
        {
            int value = this.NextDiceValue++;

            if (this.NextDiceValue > 100)
            {
                this.NextDiceValue = 1;
            }

            return value;
        }
    }
}

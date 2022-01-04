// -----------------------------------------------------------------------
// <copyright file="OneTurnDice.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dirac
{
    internal class OneTurnDice : AbstractDice
    {
        internal OneTurnDice(string rolls)
        {
            this.Rolls = rolls;
        }

        internal string Rolls { get; }

        protected override int GetRollValue(int playerIndex)
        {
            return this.Rolls[playerIndex] - '0';
        }
    }
}

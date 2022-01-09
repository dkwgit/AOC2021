// -----------------------------------------------------------------------
// <copyright file="IGame.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dirac
{
    internal interface IGame
    {
        (int Win, long UniverseCount) DoTurn();

        int GetRollResult(int playerIndex);
    }
}
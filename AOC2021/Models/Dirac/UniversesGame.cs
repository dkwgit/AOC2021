// -----------------------------------------------------------------------
// <copyright file="UniversesGame.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dirac
{
    internal class UniversesGame : Game
    {
        private static readonly int[] ExpandedRollTable = new int[] { -1, -1, -1, 1, 3, 6, 7, 6, 3, 1 };

        private long universes = 1;

        internal UniversesGame(IDice dice, (int Player1Position, int Player2Position) playerPositions, (int Player1Score, int Player2Score) playerScores, int winningScore, int numberOfRolls, long universes = 1)
            : base(dice, playerPositions, playerScores, winningScore, numberOfRolls)
        {
            this.universes = universes;
        }

        internal long Universes => this.universes;

        internal static UniversesGame CreateNewGame(UniversesGame game, string roll)
        {
            return new UniversesGame(new OneTurnDice(roll), game.PlayerPositions, game.PlayerScores, game.WinningScore, game.NumberOfRolls, game.Universes);
        }

        internal override int GetRollResult(int playerIndex)
        {
            int result = this.Dice.RollDice(this.NumberOfRolls, playerIndex);
            this.universes *= ExpandedRollTable[result];
            return result;
        }
    }
}

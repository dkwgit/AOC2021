// -----------------------------------------------------------------------
// <copyright file="Game.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dirac
{
    using System.Linq;

    internal class Game
    {
        private int turn;

        internal Game(IDice dice, List<int> playerPositions)
        {
            this.Dice = dice;
            this.PlayerPositions = playerPositions;
            this.PlayerScores = new() { 0, 0 };
            this.turn = 0;
        }

        internal IDice Dice { get; }

        internal List<int> PlayerPositions { get; }

        internal List<int> PlayerScores { get; }

        internal int Turn => this.turn;

        internal int DoTurn()
        {
            int noWin = -1;

            int index = 0;
            List<int> positionsCopy = new(this.PlayerPositions);
            foreach (var player in positionsCopy)
            {
                int playerPos = player;

                int sum = 0;
                for (int roll = 0; roll < 3; roll++)
                {
                    sum += this.Dice.RollDice();
                }

                playerPos += sum;
                while (playerPos > 10)
                {
                    playerPos -= 10;
                }

                this.PlayerPositions[index] = playerPos;
                int initialScore = this.PlayerScores[index];
                int currentScore = initialScore + playerPos;
                this.PlayerScores[index] = currentScore;

                if (this.PlayerScores[index] >= 1000)
                {
                    return index;
                }

                index++;
            }

            this.turn++;
            return noWin;
        }
    }
}

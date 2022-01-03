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
        private int turn = 0;

        private List<int> playerScores = new() { 0, 0 };

        private IDice? dice;

        private int win = -1;

        internal Game(IDice dice, List<int> playerPositions, int winningScore, int numberOfRolls)
        {
            this.dice = dice;
            this.StartingPlayerPositions = playerPositions;
            this.PlayerPositions = new(playerPositions);
            this.turn = 0;
            this.WinningScore = winningScore;
            this.NumberOfRolls = numberOfRolls;
        }

        internal int Win => this.win;

        internal IDice Dice
        {
            get
            {
                if (this.dice == null)
                {
                    throw new InvalidOperationException("Using Dice property before it is initialized.");
                }

                return this.dice;
            }
        }

        internal List<int> PlayerPositions { get; }

        internal List<int> PlayerScores => this.playerScores;

        internal int Turn => this.turn;

        internal int WinningScore { get; }

        internal int NumberOfRolls { get; }

        internal List<int> StartingPlayerPositions { get; }

        internal void Reset()
        {
            this.win = -1;
            this.playerScores = new() { 0, 0 };
            this.turn = 0;
            this.PlayerPositions[0] = this.StartingPlayerPositions[0];
            this.PlayerPositions[1] = this.StartingPlayerPositions[1];
        }

        internal void Reset(IDice dice)
        {
            this.win = -1;
            this.dice = dice;
            this.playerScores = new() { 0, 0 };
            this.turn = 0;
            this.PlayerPositions[0] = this.StartingPlayerPositions[0];
            this.PlayerPositions[1] = this.StartingPlayerPositions[1];
        }

        internal int DoTurns(int turnCount)
        {
            for (int turn = 0; turn < turnCount; turn++)
            {
                this.DoTurn();
                if (this.Win != -1)
                {
                    break;
                }
            }

            return this.Win;
        }

        internal int DoTurn()
        {
            int index = 0;
            List<int> positionsCopy = new(this.PlayerPositions);
            foreach (var player in positionsCopy)
            {
                int playerPos = player;

                int sum = this.Dice.RollDice(this.NumberOfRolls, index);

                playerPos += sum;
                while (playerPos > 10)
                {
                    playerPos -= 10;
                }

                this.PlayerPositions[index] = playerPos;
                int initialScore = this.PlayerScores[index];
                int currentScore = initialScore + playerPos;
                this.PlayerScores[index] = currentScore;

                if (this.PlayerScores[index] >= this.WinningScore)
                {
                    this.win = index;
                    return this.win;
                }

                index++;
            }

            this.turn++;
            return this.win;
        }
    }
}

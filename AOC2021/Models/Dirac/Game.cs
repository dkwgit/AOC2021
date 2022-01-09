// -----------------------------------------------------------------------
// <copyright file="Game.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dirac
{
    using System.Linq;

    internal class Game : IGame
    {
        private readonly IDice dice;

        private (int Player1Score, int Player2Score) playerScores = (0, 0);

        private (int Player1Position, int Player2Position) playerPositions;

        private int win = -1;

        internal Game(IDice dice, (int Player1Position, int Player2Position) playerPositions, int winningScore, int numberOfRolls)
        {
            this.dice = dice;
            this.playerPositions = playerPositions;
            this.WinningScore = winningScore;
            this.NumberOfRolls = numberOfRolls;
        }

        internal int Win => this.win;

        internal IDice Dice => this.dice;

        internal (int Player1Position, int Player2Position) PlayerPositions => this.playerPositions;

        internal (int Player1Score, int Player2Score) PlayerScores => this.playerScores;

        internal int WinningScore { get; }

        internal int NumberOfRolls { get; }

        public virtual int GetRollResult(int playerIndex)
        {
            return this.Dice.RollDice(this.NumberOfRolls, playerIndex);
        }

        public virtual (int Win, long UniverseCount) DoTurn()
        {
            int win = this.Turn();
            return (win, 1L);
        }

        protected int Turn()
        {
            (int player1Position, int player2Position) = this.PlayerPositions;
            (int player1Score, int player2Score) = this.PlayerScores;

            for (int player = 0; player < 2; player++)
            {
                int position = player == 0 ? player1Position : player2Position;

                int sum = this.GetRollResult(player);

                position += sum;
                while (position > 10)
                {
                    position -= 10;
                }

                if (player == 0)
                {
                    player1Position = position;
                }
                else
                {
                    player2Position = position;
                }

                int initialScore = player == 0 ? player1Score : player2Score;
                int newScore = initialScore + position;

                if (player == 0)
                {
                    player1Score = newScore;
                }
                else
                {
                    player2Score = newScore;
                }

                if (newScore >= this.WinningScore)
                {
                    this.win = player;
                    break;
                }
            }

            this.playerPositions = (player1Position, player2Position);
            this.playerScores = (player1Score, player2Score);
            return this.win;
        }
    }
}

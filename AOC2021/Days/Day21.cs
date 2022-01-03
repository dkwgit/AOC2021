// -----------------------------------------------------------------------
// <copyright file="Day21.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Runtime.CompilerServices;
    using AOC2021.Data;
    using AOC2021.Models.Dirac;

    internal class Day21 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        private readonly List<int> startingPlayerPositions = new();

        public Day21(DataStore datastore)
        {
            this.datastore = datastore;
        }

        internal List<int> StartingPlayerPositions => this.startingPlayerPositions;

        public override string GetDescription()
        {
            return "Dirac Dice";
        }

        public override string Result1()
        {
            this.PrepData();

            Game game = new(new DeterministicDice(), this.StartingPlayerPositions, 1000, 3);

            int win = -1;

            while (win == -1)
            {
                win = game.DoTurn();
            }

            int losingScore = game.PlayerScores.OrderBy(s => s).First();
            int rollCount = game.Dice.RollCount;

            long result = losingScore * rollCount;
            return result.ToString();
        }

        public override string Result2()
        {

            long player1UniverseWins = 0, player2UniverseWins = 0;
            long result = Math.Max(player1UniverseWins, player2UniverseWins);
            return result.ToString();
        }

        internal void PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());

            foreach (string line in lines)
            {
                char[] chars = line.ToCharArray();
                this.StartingPlayerPositions.Add(chars[^1] - '0');
            }
        }
    }
}

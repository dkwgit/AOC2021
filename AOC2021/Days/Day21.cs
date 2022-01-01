// -----------------------------------------------------------------------
// <copyright file="Day21.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;
    using AOC2021.Models.Dirac;

    internal class Day21 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        private Game? game;

        public Day21(DataStore datastore)
        {
            this.datastore = datastore;
        }

        internal Game Game
        {
            get
            {
                if (this.game == null)
                {
                    throw new InvalidOperationException("Use of Game property before initialized");
                }

                return this.game;
            }
        }

        public override string GetDescription()
        {
            return "Dirac Dice";
        }

        public override string Result1()
        {
            this.PrepData();

            int win = -1;

            while (win == -1)
            {
                win = this.Game.DoTurn();
            }

            int losingScore = this.Game.PlayerScores.OrderBy(s => s).First();
            int rollCount = this.Game.Dice.RollCount;

            long result = losingScore * rollCount;
            return result.ToString();
        }

        public override string Result2()
        {
            long result = 0;
            return result.ToString();
        }

        internal void PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());

            List<int> playerPositions = new();
            foreach (string line in lines)
            {
                char[] chars = line.ToCharArray();
                playerPositions.Add(chars[^1] - '0');
            }

            this.game = new Game(new DeterministicDice(), playerPositions, 1000);
        }
    }
}

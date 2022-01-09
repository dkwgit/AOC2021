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

        private readonly List<int> startingPlayerPositions = new();

        private readonly int[] combinationCountsForDiceValues = new int[] { -1, -1, -1, 1, 3, 6, 7, 6, 3, 1 };

        private long player1Wins = 0;

        private long player2Wins = 0;

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

            Game game = new(new DeterministicDice(), (this.StartingPlayerPositions[0], this.StartingPlayerPositions[1]), 1000, 3);

            int win = -1;

            while (win == -1)
            {
                (win, _) = game.DoTurn();
            }

            int losingScore = Math.Min(game.PlayerScores.Player1Score, game.PlayerScores.Player2Score);
            int rollCount = game.Dice.RollCount;

            long result = losingScore * rollCount;
            return result.ToString();
        }

        public override string Result2()
        {
            int[] diceValues = new int[] { 3, 4, 5, 6, 7, 8, 9 };

            int startScore1 = 0;
            int startScore2 = 0;
            int startPosition1 = 4;
            int startPosition2 = 3;

            var startingInstancesToProcess =
                from roll in diceValues
                select (startScore1, startScore2, startPosition1, startPosition2, 1L, roll, 1);

            List<IEnumerable<(int Score1, int Score2, int Position1, int Position2, long InstanceCount, int Roll, int Player)>> nextTurnInputs = new();
            nextTurnInputs.Add(startingInstancesToProcess);

            // showed myself on paper that 10 turns for both players are the maximum.
            for (int turn = 1; turn <= 20; turn++)
            {
                var nextTurnInput = nextTurnInputs[^1].
                    /* When processinstance returns, the result contains the index of the opposite player, so that calls for player 1 and 2 alternate back and forth */
                    Select(instance => this.ProcessInstance(instance.Score1, instance.Score2, instance.Position1, instance.Position2, instance.InstanceCount, instance.Roll, instance.Player)).
                    Where(r => r.Win == -1).
                    SelectMany(result => diceValues, (result, roll) => (result.Score1, result.Score2, result.Position1, result.Position2, result.InstanceCount, roll, result.Player));

                nextTurnInputs.Add(nextTurnInput);
            }

            long count = nextTurnInputs[^1].ToList().Count; // Everything in the IEnumerables in is lazy, this forces evaluation, back through the turns

            long result = Math.Max(this.player1Wins, this.player2Wins);
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

        private static (int Win, int Score, int Position) ProcessPlayer(int score, int position, int roll, int player)
        {
            position = (position + roll > 10) ? position + roll - 10 : position + roll;
            score += position;
            int win = -1;
            if (score >= 21)
            {
                win = player;
            }

            return (win, score, position);
        }

        private (int Win, int Score1, int Score2, int Position1, int Position2, long InstanceCount, int Player) ProcessInstance(int score1, int score2, int position1, int position2, long instanceCount, int roll, int player)
        {
            int comboCount = this.combinationCountsForDiceValues[roll];

            int win;
            if (player == 1)
            {
                (win, score1, position1) = ProcessPlayer(score1, position1, roll, player);
            }
            else
            {
                (win, score2, position2) = ProcessPlayer(score2, position2, roll, player);
            }

            instanceCount *= comboCount;
            if (win != -1)
            {
                if (player == 1)
                {
                    this.player1Wins += instanceCount;
                }
                else
                {
                    this.player2Wins += instanceCount;
                }
            }

            return (win, score1, score2, position1, position2, instanceCount, player == 1 ? 2 : 1);
        }
    }
}

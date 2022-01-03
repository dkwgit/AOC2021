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
            List<int> sums = new() { 3, 4, 5, 6, 7, 8, 9 };

            (int, int) badPair = new(-1, -1);

            var combos =
            from a in sums
            from b in sums
            select (a, b);

            var rollCombos =
            from x in combos
            from y in combos
            from z in combos
            select new ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))>
            (
                x, y, z, badPair, badPair, badPair, badPair, (badPair, badPair, badPair)
            );

            List<ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))>> unResolvedGameRolls = new();

            long player1Win = 0;
            long player2Win = 0;
            long gamesPlayed = 0;

            long universesCreated = 0;
            long player1UniverseWins = 0;
            long player2UniverseWins = 0;
            for (int numberOfTurns = 3; numberOfTurns <= 10; numberOfTurns++)
            {
                long gamesThisRound = 0;
                Console.WriteLine($"Processing {numberOfTurns} games");
                List<ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))>> gameRollInfo = new(rollCombos);
                unResolvedGameRolls.Clear();
                long winCount = 0;
                long noWinCount = 0;
                foreach (var rollInfo in gameRollInfo)
                {
                    gamesThisRound++;
                    Game game = new(new PreloadedDice(rollInfo), this.StartingPlayerPositions, 21, 1);
                    int win = game.DoTurns(numberOfTurns);
                    if (win == -1)
                    {
                        unResolvedGameRolls.Add(rollInfo);
                        noWinCount++;
                    }
                    else
                    {
                        gamesPlayed++;

                        long universes = this.GetUniverseCounts(rollInfo, numberOfTurns);
                        universesCreated += universes;
                        if (win == 0)
                        {
                            player1Win++;
                            player1UniverseWins += universes;
                        }
                        else
                        {
                            player2Win++;
                            player2UniverseWins += universes;
                        }

                        winCount++;
                    }
                }

                int nextTurnIndex = numberOfTurns + 1;

                Console.WriteLine($"Done processing {numberOfTurns} games. Games this round: {gamesThisRound}. Universe count is at {universesCreated}. Number of unresolved games {unResolvedGameRolls.Count()}. Next round is going to have to play {unResolvedGameRolls.Count() * 49}");
                if (nextTurnIndex <= 10)
                {
                    rollCombos =
                         new List<ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))>>(unResolvedGameRolls).SelectMany(rollInfo => new List<(int, int)>(combos), (rollInfo, combo) =>
                         {
                             return this.TupleValueSetter(rollInfo, nextTurnIndex, combo);
                         });
                }
            }

            Console.WriteLine($"Universes created : {universesCreated}");
            Console.WriteLine($"Player1 universe wins : {player1UniverseWins}");
            Console.WriteLine($"Player2 universe wins : {player2UniverseWins}");
            long result = Math.Max(player1UniverseWins, player2UniverseWins);
            return result.ToString();
        }

        internal ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))> TupleValueSetter(
            ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))> t, int index, (int, int) pair)
        {
            ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int),(int, int))> returnValue;

            returnValue = index switch
            {
                4 => new ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))>(t.Item1, t.Item2, t.Item3, pair, t.Item5, t.Item6, t.Item7, t.Rest),
                5 => new ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))>(t.Item1, t.Item2, t.Item3, t.Item4, pair, t.Item6, t.Item7, t.Rest),
                6 => new ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))>(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, pair, t.Item7, t.Rest),
                7 => new ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))>(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6, pair, t.Rest),
                8 => new ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))>(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6, t.Item7, (pair, t.Rest.Item2, t.Rest.Item3)),
                9 => new ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))>(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6, t.Item7, (t.Rest.Item1, pair, t.Rest.Item3)),
                10 => new ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))>(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6, t.Item7, (t.Rest.Item1, t.Rest.Item2, pair)),
                _ => throw new InvalidOperationException(),
            };

            return returnValue;
        }
        internal long GetUniverseCounts(ValueTuple<(int, int), (int, int), (int, int), (int, int), (int, int), (int, int), (int, int), ((int, int), (int, int), (int, int))> rollInfo, int turns)
        {
            long universes = 1;

            for (int t = 1; t <= turns; t++)
            {
                int turnValue = 1;
                (int player1Roll, int player2Roll) = t switch
                {
                    1 => rollInfo.Item1,
                    2 => rollInfo.Item2,
                    3 => rollInfo.Item3,
                    4 => rollInfo.Item4,
                    5 => rollInfo.Item5,
                    6 => rollInfo.Item6,
                    7 => rollInfo.Item7,
                    8 => rollInfo.Rest.Item1,
                    9 => rollInfo.Rest.Item2,
                    10 => rollInfo.Rest.Item3,
                    _ => throw new InvalidOperationException(),
                };
                for (int p = 0; p < 2; p++)
                {
                    int roll;
                    if (p == 0)
                    {
                        roll = player1Roll;
                    }
                    else
                    {
                        roll = player2Roll;
                    }

                    turnValue *= roll switch
                    {
                        3 => 1 * 27, // 1 way  to get a 3 in 3 rolls
                        4 => 3 * 27, // 3 ways to get a 4 in 3 rolls
                        5 => 6 * 27, // 6 ways to get a 5 in 3 rolls
                        6 => 7 * 27, // 7 ways to get a 6 in 3 rolls
                        7 => 6 * 27, // 6 ways to get a 7 in 3 rolls
                        8 => 3 * 27, // 3 ways to get a 8 in 3 rolls
                        9 => 1 * 27, // 1 way  to get a 9 in 3 rolls
                        _ => throw new InvalidOperationException(),
                    };
                }

                universes *= turnValue;
            }

            return universes;
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

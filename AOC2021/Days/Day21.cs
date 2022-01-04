// -----------------------------------------------------------------------
// <copyright file="Day21.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Collections.Concurrent;
    using System.Threading;
    using AOC2021.Data;
    using AOC2021.Models.Dirac;
    using FluentAssertions;

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

            Game game = new(new DeterministicDice(), (this.StartingPlayerPositions[0], this.StartingPlayerPositions[1]), 1000, 3);

            int win = -1;

            while (win == -1)
            {
                win = game.DoTurn();
            }

            int losingScore = Math.Min(game.PlayerScores.Player1Score, game.PlayerScores.Player2Score);
            int rollCount = game.Dice.RollCount;

            long result = losingScore * rollCount;
            return result.ToString();
        }

        public override string Result2()
        {
            long universes = 0, player1UniverseWins = 0, player2UniverseWins = 0;

            string[] diceValues = new string[] { "3", "4", "5", "6", "7", "8", "9" };

            var rollCombo =
                from a in diceValues
                from b in diceValues
                select a + b;

            var gamesToPlay =
                from combo in rollCombo
                select new UniversesGame(new OneTurnDice(combo), (this.StartingPlayerPositions[0], this.StartingPlayerPositions[1]), (0, 0), 21, 1);

            List<UniversesGame> unresolved = new();
            long totalGames = 0;
            long totalGamesFinished = 0;
            for (int turn = 1; turn <= 10; turn++)
            {
                long gameCounter = 0;
                long gamesFinished = 0;
                long unresolvedCount = unresolved.Count;

                unresolved.Clear();

                Console.WriteLine($@"
On turn: {turn}.
    Universes {universes}
    Player 1 win universes: {player1UniverseWins}
    Player 2 win universes: {player2UniverseWins}
    Unresolved contains {unresolvedCount}
    This turn will process {unresolvedCount * 49} games
    Total games started so far: {totalGames}
    Total games finished so far: {totalGamesFinished}");

                foreach (var game in gamesToPlay)
                {
                    gameCounter++;
                    totalGames++;
                    if (gameCounter % 1000000 == 0)
                    {
                        Console.WriteLine($"\t\tGames {gameCounter} processed so far. Total finished {gamesFinished}");
                    }

                    int win = game.DoTurn();
                    if (win != -1)
                    {
                        gamesFinished++;

                        universes += game.Universes;
                        if (win == 0)
                        {
                            player1UniverseWins += game.Universes;
                        }
                        else
                        {
                            player2UniverseWins += game.Universes;
                        }
                    }
                    else
                    {
                        unresolved.Add(game);
                    }
                }

                Console.WriteLine($"\tTurn {turn} is finished. Total games this turn: {gameCounter} processed so far. Total games {gamesFinished}.");

                totalGamesFinished += gamesFinished;
                List<UniversesGame> continuingGames = new(unresolved);

                gamesToPlay =
                    from combo in rollCombo
                    from g in continuingGames
                    select UniversesGame.CreateNewGame(g, combo);
            }

            unresolved.Count.Should().Be(0);

            Console.WriteLine($"\tCalculations finished. Total games: {totalGames} processed so far. Total games  {totalGamesFinished}.");

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

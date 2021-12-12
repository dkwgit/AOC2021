// -----------------------------------------------------------------------
// <copyright file="Day04.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Text.RegularExpressions;
    using AOC2021.Data;
    using AOC2021.Models.Bingo;
    using FluentAssertions;

    /// <summary>
    /// Day 4: playing bingo with a Giant Squid.
    /// </summary>
    public class Day04 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day04(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public override string GetDescription()
        {
            return "Playing bingo with a giant squid.";
        }

        public override long Result1()
        {
            long result = -1;
            (int[] numbers, BingoBoard[] boards) = this.PrepData();

            int visitRound = 0;
            int visitNumber = 0;

            foreach (int number in numbers)
            {
                foreach (var board in boards)
                {
                    if (board.AnnounceNumberAndCheck(number, visitNumber++))
                    {
                        result = board.CalculateWinResult();
                        return result;
                    }
                }

                visitRound++;
            }

            return result;
        }

        public override long Result2()
        {
            (int[] numbers, BingoBoard[] boards) = this.PrepData();
            int visitNumber = 0;

            foreach (int number in numbers)
            {
                foreach (var board in boards)
                {
                    board.AnnounceNumberAndCheck(number, visitNumber++);
                }
            }

            var lastWinningBoard = boards.Where(b => b.WinningNumber != -1).Select(b => b).OrderByDescending(b => b.VisitNumber).First();

            long result = lastWinningBoard.CalculateWinResult();
            return result;
        }

        private (int[] Numbers, BingoBoard[] Boards) PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());
            List<BingoBoard> boards = new();

            int[] numbers = lines[0].Split(",").Select(x =>
            {
                bool tryResult = int.TryParse(x, out int number);
                if (!tryResult)
                {
                    throw new InvalidDataException($"String {x} was expected to be parsable into an int, but was not.");
                }

                return number;
            }).ToArray();

            int lineNumber = 1;
            int[,] boardData = new int[5, 5];
            string pattern = @"((?: \d)|\d{2}) ?";
            while (lineNumber < lines.Length)
            {
                if (string.IsNullOrEmpty(lines[lineNumber++]))
                {
                    ;
                }

                for (int row = 0; row < 5; row++)
                {
                    string line = lines[lineNumber++];
                    var matches = Regex.Matches(line, pattern);
                    matches.Count.Should().Be(5);

                    for (int column = 0; column < 5; column++)
                    {
                        string bingoNumber = matches[column].Value;
                        bool tryResult = int.TryParse(bingoNumber, out int number);
                        if (!tryResult)
                        {
                            throw new InvalidDataException($"String bingoNumber {bingoNumber} was expected to be parsable into an int, but was not.");
                        }

                        boardData[row, column] = number;
                    }
                }

                boards.Add(new BingoBoard(boardData));

                boardData = new int[5, 5];
            }

            return (numbers, boards.ToArray());
        }
    }
}

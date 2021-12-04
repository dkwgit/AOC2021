// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AOC2021.Bingo;
    using FluentAssertions;

    /// <summary>
    /// Entry class.
    /// </summary>
    public class Program
    {
        private static int[] Day_01_01_Input { get; set; } = Process_Day_01_01_Input();

        private static List<(int Forward, int Vertical)> Day_02_01_Input { get; set; } = Process_Day_02_01_Input();

        private static List<(int Forward, int Vertical)> Day_02_02_Input { get; set; } = Process_Day_02_02_Input();

        private static (List<BitVector32> DiagCodes, int DiagColumns) Day_03_01_Input { get; set; } = Process_Day_03_01_Input();

        private static (List<int> Numbers, List<BingoBoard> Boards) Day_04_01_Input { get; set; } = Process_Day_04_01_Input();

        /// <summary>
        /// Standard console entry function.
        /// </summary>
        /// <!-- param name="args">Command line args.</param-->
        public static void Main(/* string[] args*/)
        {
            Stopwatch sw = new();
            sw.Start();
            int result_01_01 = Day_01_01();
            int result_01_01_actual = 1167;
            result_01_01.Should().Be(result_01_01_actual, "Day_01_01 has a wrong result");
            int result_01_02 = Day_01_02();
            int result_01_02_actual = 1130;
            result_01_02.Should().Be(result_01_02_actual, "Day_01_02 has a wrong result");
            int result_01_02_Variant = Day_01_02_Variant();
            result_01_02_Variant.Should().Be(result_01_02_actual, "Day_01_02_Variant has a wrong result");
            int result_02_01 = Day_02_01();
            int result_02_01_actual = 1947824;
            result_02_01.Should().Be(result_02_01_actual, "Day_02_01 has a wrong result");
            int result_02_02 = Day_02_02();
            int result_02_02_actual = 1813062561;
            result_02_02.Should().Be(result_02_02_actual, "Day_02_02 has a wrong result");
            int result_03_01 = Day_03_01();
            int result_03_01_actual = 2967914;
            result_03_01.Should().Be(result_03_01_actual, "Day_03_01 has a wrong result");
            int result_03_02 = Day_03_02();
            int result_03_02_actual = 7041258;
            result_03_02.Should().Be(result_03_02_actual, "Day_03_02 has a wrong result");
            int result_04_01 = Day_04_01();
            int result_04_01_actual = 39984;
            result_04_01.Should().Be(result_04_01_actual, "Day_04_01 has a wrong result");
            int result_04_02 = Day_04_02();
            int result_04_02_actual = 8468;
            result_04_02.Should().Be(result_04_02_actual, "Day_04_01 has a wrong result");
            sw.Stop();
            Console.WriteLine($"Total run time in ms: {sw.ElapsedMilliseconds}");
        }

        private static int[] Process_Day_01_01_Input()
        {
            return File.ReadAllLines("./Day_01_01.txt").Select(x =>
            {
                bool tryResult = int.TryParse(x, out int result);
                if (!tryResult)
                {
                    throw new InvalidDataException($"String x {x} was expected to be parsable into an int, but was not.");
                }

                return result;
            }).ToArray();
        }

        private static List<(int Forward, int Vertical)> Process_Day_02_01_Input()
        {
            return File.ReadAllLines("./Day_02_01.txt").Select(s =>
            {
                int forward = 0;
                int vertical = 0;

                if (s.Contains("forward"))
                {
                    s = s.Replace("forward ", string.Empty);
                    bool tryResult = int.TryParse(s, out forward);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String bingoNumber {s} was expected to be parsable into an int, but was not.");
                    }
                }

                if (s.Contains("up"))
                {
                    s = s.Replace("up ", string.Empty);
                    bool tryResult = int.TryParse(s, out vertical);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String bingoNumber {s} was expected to be parsable into an int, but was not.");
                    }

                    vertical *= -1;
                }

                if (s.Contains("down"))
                {
                    s = s.Replace("down ", string.Empty);
                    bool tryResult = int.TryParse(s, out vertical);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String bingoNumber {s} was expected to be parsable into an int, but was not.");
                    }
                }

                return (forward, vertical);
            }).ToList();
        }

        private static List<(int Forward, int Vertical)> Process_Day_02_02_Input()
        {
            int aim = 0;

            return File.ReadAllLines("./Day_02_01.txt").Select(s =>
            {
                int forward = 0;
                int vertical = 0;
                int aimChange = 0;

                if (s.Contains("forward"))
                {
                    s = s.Replace("forward ", string.Empty);
                    bool tryResult = int.TryParse(s, out forward);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String s {s} was expected to be parsable into an int, but was not.");
                    }

                    vertical = aim * forward;
                }

                if (s.Contains("up"))
                {
                    s = s.Replace("up ", string.Empty);
                    bool tryResult = int.TryParse(s, out aimChange);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String bingoNumber {s} was expected to be parsable into an int, but was not.");
                    }

                    aim -= aimChange;
                }

                if (s.Contains("down"))
                {
                    s = s.Replace("down ", string.Empty);
                    bool tryResult = int.TryParse(s, out aimChange);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String bingoNumber {s} was expected to be parsable into an int, but was not.");
                    }

                    aim += aimChange;
                }

                return (forward, vertical);
            }).ToList();
        }

        private static (List<BitVector32> DiagRows, int DiagColumns) Process_Day_03_01_Input()
        {
            int columnCount = -1;
            var list = File.ReadAllLines("./Day_03_01.txt").Select(s =>
            {
                if (columnCount == -1)
                {
                    columnCount = s.Length;
                }
                else
                {
                    s.Length.Should().Be(columnCount);
                }

                BitVector32 bits = new(0);
                int mask = BitVector32.CreateMask();
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '1')
                    {
                        bits[mask] = s[i] == '1';
                    }

                    mask = BitVector32.CreateMask(mask);
                }

                return bits;
            }).ToList();

            return (list, columnCount);
        }

        private static (List<int> Numbers, List<BingoBoard> Boards) Process_Day_04_01_Input()
        {
            List<string> lines = File.ReadAllLines("./Day_04_01.txt").ToList();
            List<BingoBoard> boards = new();

            List<int> numbers = lines[0].Split(",").Select(x =>
            {
                bool tryResult = int.TryParse(x, out int number);
                if (!tryResult)
                {
                    throw new InvalidDataException($"String {x} was expected to be parsable into an int, but was not.");
                }

                return number;
            }).ToList();

            int lineNumber = 1;
            int[,] boardData = new int[5, 5];
            string pattern = @"((?: \d)|\d{2}) ?";
            while (lineNumber < lines.Count)
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

            return (numbers, boards);
        }

        private static int Day_01_01()
        {
            int[] depths = Day_01_01_Input;

            int result = depths.Where(
                (item, index) => (index > 0 && (depths[index - 1] < depths[index]))
            ).Count();

            Console.WriteLine($"Day_01_01 result: {result}");
            return result;
        }

        private static int Day_01_02()
        {
            int[] depths = Day_01_01_Input;

            List<int> sums = new();

            for (int i = 2; i < depths.Length; i++)
            {
                sums.Add(depths[i - 2] + depths[i - 1] + depths[i]);
            }

            int result = sums.Where(
                (item, index) => (index > 0 && (sums[index - 1] < sums[index]))
            ).Count();

            Console.WriteLine($"Day_01_02 result: {result}");
            return result;
        }

        private static int Day_01_02_Variant()
        {
            int[] depths = Day_01_01_Input;

            List<int> windowedSums = depths.WindowedTraverse(3, (int accumulate, int source) => accumulate + source).ToList();

            int result = windowedSums.Where(
                (item, index) => (index > 0 && (windowedSums[index - 1] < windowedSums[index]))
            ).Count();

            Console.WriteLine($"Day_01_02 result: {result}");
            return result;
        }

        private static int Day_02_01()
        {
            List<(int, int)> coords = Day_02_01_Input;

            (int Forward, int Vertical) aggregate = coords.Aggregate<(int, int)>(
                ((int, int) element, (int, int) accumulate) =>
                {
                    (int forward, int vertical) = element;
                    (int f, int v) = accumulate;
                    return (forward + f, vertical + v);
                });

            int result = aggregate.Forward * aggregate.Vertical;
            Console.WriteLine($"Day_02_01 result: {result}");
            return result;
        }

        private static int Day_02_02()
        {
            List<(int, int)> coords = Day_02_02_Input;

            (int Forward, int Vertical) aggregate = coords.Aggregate<(int, int)>(
                ((int, int) element, (int, int) accumulate) =>
                {
                    (int forward, int vertical) = element;
                    (int f, int v) = accumulate;
                    return (forward + f, vertical + v);
                });

            int result = aggregate.Forward * aggregate.Vertical;
            Console.WriteLine($"Day_02_02 result: {result}");
            return result;
        }

        private static int Day_03_01()
        {
            (List<BitVector32> diagRows, int diagColumnCount) = Day_03_01_Input;
            int diagRowCount = diagRows.Count;

            BitVector32 epsilon = new(0);
            int[] columnSums = new int[diagColumnCount];

            int mask;
            for (int r = 0; r < diagRows.Count; r++)
            {
                var row = diagRows[r];
                mask = BitVector32.CreateMask();
                for (int c = 0; c < diagColumnCount; c++)
                {
                    columnSums[c] += row[mask] ? 1 : 0;
                    mask = BitVector32.CreateMask(mask);
                }
            }

            mask = BitVector32.CreateMask();
            for (int c = 0; c < diagColumnCount; c++)
            {
                epsilon[mask] = columnSums[c] > (diagRowCount / 2);
                mask = BitVector32.CreateMask(mask);
            }

            int epsilonAsInt = 0;
            int gammaAsInt = 0;
            mask = BitVector32.CreateMask();
            for (int c = 0; c < diagColumnCount; c++)
            {
                int bit = epsilon[mask] ? 1 : 0;

                epsilonAsInt |= bit;
                gammaAsInt |= bit == 1 ? 0 : 1;

                if (c + 1 < diagColumnCount)
                {
                    epsilonAsInt <<= 1;
                    gammaAsInt <<= 1;
                }

                mask = BitVector32.CreateMask(mask);
            }

            int result = epsilonAsInt * gammaAsInt;
            Console.WriteLine($"Day_03_01 result: {result}");
            return result;
        }

        private static int Day_03_02()
        {
            (List<BitVector32> diags, int columnCount) = Day_03_01_Input;

            List<BitVector32> oxygenItems = new(diags);
            List<BitVector32> scrubberItems = new(diags);

            int mask = BitVector32.CreateMask();
            for (int c = 0; c < columnCount; c++)
            {
                var groupings = oxygenItems.
                    GroupBy(x => x[mask]).
                    Select(x => new { Value = x.Key, Count = x.Count() }).
                    OrderByDescending(x => x.Count).
                    ThenByDescending(x => x.Value).ToArray();

                bool searchValue = groupings[0].Value;

                oxygenItems = oxygenItems.Where(x => x[mask] == searchValue).ToList();
                if (oxygenItems.Count == 1)
                {
                    break;
                }

                mask = BitVector32.CreateMask(mask);
            }

            mask = BitVector32.CreateMask();
            for (int c = 0; c < columnCount; c++)
            {
                var groupings = scrubberItems.
                     GroupBy(x => x[mask]).
                     Select(x => new { Value = x.Key, Count = x.Count() }).
                     OrderBy(x => x.Count).
                     ThenBy(x => x.Value).ToArray();

                bool searchValue = groupings[0].Value;

                scrubberItems = scrubberItems.Where(x => x[mask] == searchValue).ToList();
                if (scrubberItems.Count == 1)
                {
                    break;
                }

                mask = BitVector32.CreateMask(mask);
            }

            int oxygenNumber = 0;
            int scrubberNumber = 0;
            mask = BitVector32.CreateMask();
            for (int i = 0; i < columnCount; i++)
            {
                oxygenNumber |= oxygenItems[0][mask] ? 1 : 0;
                scrubberNumber |= scrubberItems[0][mask] ? 1 : 0;
                if (i + 1 < columnCount)
                {
                    oxygenNumber <<= 1;
                    scrubberNumber <<= 1;
                }

                mask = BitVector32.CreateMask(mask);
            }

            int result = oxygenNumber * scrubberNumber;
            Console.WriteLine($"Day_03_02 result: {result}");
            return result;
        }

        private static int Day_04_01()
        {
            int result = -1;
            (List<int> numbers, List<BingoBoard> boards) = Day_04_01_Input;

            int visitRound = 0;
            int visitNumber = 0;

            foreach (int number in numbers)
            {
                foreach (var board in boards)
                {
                    if (board.AnnounceNumberAndCheck(number, visitNumber++))
                    {
                        result = board.CalculateWinResult();
                        Console.WriteLine($"Day_04_01 result: {result}");
                        return result;
                    }
                }

                visitRound++;
            }

            Console.WriteLine($"Day_04_01: No win found!");
            return result;
        }

        private static int Day_04_02()
        {
            (List<int> numbers, List<BingoBoard> boards) = Program.Day_04_01_Input;

            int visitRound = 0;
            int visitNumber = 0;

            foreach (int number in numbers)
            {
                foreach (var board in boards)
                {
                    board.AnnounceNumberAndCheck(number, visitNumber++);
                }

                visitRound++;
            }

            var lastWinningBoard = boards.Where(b => b.WinningNumber != -1).Select(b => b).OrderByDescending(b => b.VisitNumber).First();
            int result = lastWinningBoard.CalculateWinResult();

            Console.WriteLine($"Day_04_02 result: {result}");
            return result;
        }
    }
}
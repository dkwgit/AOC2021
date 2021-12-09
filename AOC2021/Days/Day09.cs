// -----------------------------------------------------------------------
// <copyright file="Day09.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;
    using FluentAssertions;

    /// <summary>
    /// Day 9: Minima.
    /// </summary>
    public class Day09 : IDay
    {
        private readonly DataStore datastore;

        public Day09(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public string Name { get; init; } = "Day09";

        public long Result1()
        {
            int[,] map = this.PrepData();

            long result = this.FindRiskSum(map);
            return result;
        }

        public long Result2()
        {
            long result = 0;
            return result;
        }

        private int FindRiskSum(int[,] map)
        {
            int rows = map.GetUpperBound(0) + 1;
            int columns = map.GetUpperBound(1) + 1;
            int riskSum = 0;
            List<(int Row, int Column, int Value, int Risk)> minima = new();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    int value = map[r, c];
                    if (this.IsMinimum(map, r, c, rows, columns))
                    {
                        minima.Add((r, c, value, value + 1));
                    }
                }
            }

            riskSum = minima.Sum(x => x.Risk);

            return riskSum;
        }

        private bool IsMinimum(int[,] map, int r, int c, int rows, int columns)
        {
            List<bool> checks = new();

            int value = map[r, c];

            if (r > 0)
            {
                // Up
                checks.Add(value < map[r - 1, c]);
            }

            if (r + 1 < rows)
            {
                // Down
                checks.Add(value < map[r + 1, c]);
            }

            if (c > 0)
            {
                // Left
                checks.Add(value < map[r, c - 1]);
            }

            if (c + 1 < columns)
            {
                // Right
                checks.Add(value < map[r, c + 1]);
            }

            return checks.All(x => x);
        }

        private int[,] PrepData()
        {
            string[] lines = this.datastore.GetRawData("09");
            int rows = lines.Length;
            int columns = lines[0].Length;
            int[,] data = new int[rows, columns];

            for (int r = 0; r < rows; r++)
            {
                string row = lines[r];
                row.Length.Should().Be(columns);
                char[] chars = row.ToCharArray();

                for (int c = 0; c < columns; c++)
                {
                    char ch = chars[c];
                    ch.Should().BeInRange('0', '9');
                    data[r, c] = chars[c] - '0';
                }
            }

            return data;
        }
    }
}

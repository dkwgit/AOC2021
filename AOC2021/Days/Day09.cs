// -----------------------------------------------------------------------
// <copyright file="Day09.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Drawing;
    using System.Text;
    using AOC2021.Data;
    using AOC2021.Models;
    using FluentAssertions;
    using Pastel;

    /// <summary>
    /// Day 9: Minima and basins.
    /// </summary>
    public class Day09 : IDay
    {
        private readonly DataStore datastore;

        public Day09(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public Dictionary<Point, (int Low, int Risk)> Minima { get; set; } = new();

        public string Name { get; init; } = "Day09";

        public long Result1()
        {
            int[,] map = this.PrepData();

            long result = this.FindRiskSum(map);
            return result;
        }

        public long Result2()
        {
            bool showPlot = false;

            int[,] originalMap = this.PrepData();

            // We're goign to add a border of 9s to help the algorithm
            int[,] paddedMap = new int[originalMap.GetUpperBound(0) + 3, originalMap.GetUpperBound(1) + 3];

            for (int r = 0; r < paddedMap.GetUpperBound(0) + 1; r++)
            {
                for (int c = 0; c < paddedMap.GetUpperBound(1) + 1; c++)
                {
                    if (r == 0 || r == paddedMap.GetUpperBound(0) || c == 0 || c == paddedMap.GetUpperBound(1))
                    {
                        paddedMap[r, c] = 9;
                    }
                    else
                    {
                        paddedMap[r, c] = originalMap[r - 1, c - 1];
                    }
                }
            }

            // Because of added border adjust the coordinates of the minima
            Dictionary<Point, (int Low, int Risk)> adjustedMinima = new();
            foreach (Point point in this.Minima.Keys)
            {
                adjustedMinima[new Point(point.X + 1, point.Y + 1)] = this.Minima[point];
            }

            this.Minima = adjustedMinima;

            if (showPlot)
            {
                this.PlotRidges(paddedMap);
            }

            Basin[] basins = new Basin[this.Minima.Count];
            int i = 0;
            foreach (Point p in this.Minima.Keys)
            {
                Basin b = new(paddedMap, p);
                basins[i++] = b;
                b.MapBasin();
            }

            Basin[] topBasins = basins.OrderByDescending(b => b.BasinPoints.Count).Take(3).ToArray();
            long result = topBasins.Aggregate(1L, (a, b) => a * b.BasinPoints.Count);
            return result;
        }

        private int FindRiskSum(int[,] map)
        {
            int rows = map.GetUpperBound(0) + 1;
            int columns = map.GetUpperBound(1) + 1;
            int riskSum = 0;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    int value = map[r, c];
                    if (this.IsMinimum(map, r, c, rows, columns))
                    {
                        this.Minima[new Point(c, r)] = (Low: value, Risk: value + 1);
                    }
                }
            }

            riskSum = this.Minima.Sum(m => m.Value.Risk);

            return riskSum;
        }

        private void PlotRidges(int[,] map)
        {
            int rows = map.GetUpperBound(0) + 1;
            int columns = map.GetUpperBound(1) + 1;

            for (int r = 0; r < rows; r++)
            {
                StringBuilder sb = new();
                for (int c = 0; c < columns; c++)
                {
                    string s = map[r, c].ToString();
                    if (s == "9")
                    {
                        s = s.Pastel(Color.Black).PastelBg("E34234");
                    }
                    else if (this.Minima.ContainsKey(new Point(c, r)))
                    {
                        s = s.Pastel(Color.Black).PastelBg("FEFF60");
                    }

                    sb.Append(s);
                    if (c + 1 == columns)
                    {
                        sb.Append(" ".Pastel(Color.Black));
                    }
                }

                Console.WriteLine(sb);
            }
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
            string[] lines = this.datastore.GetRawData(this.Name);
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

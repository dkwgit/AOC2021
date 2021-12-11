// -----------------------------------------------------------------------
// <copyright file="Day11.cs" company="David Wright">
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

    /// <summary>
    /// Day 2: Submarine depth calculation, etc.
    /// </summary>
    public class Day11 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day11(DataStore datastore)
        {
            this.datastore = datastore;
            this.Cells = this.PrepData();
            this.Size = this.Cells.GetUpperBound(0) + 1;
            (this.Cells.GetUpperBound(1) + 1).Should().Be(this.Size);
        }

        private Cell[,] Cells { get; set;  }

        private int Size { get; }

        private bool PrintCells { get; } = false;

        private FlashCounter Counter { get; set; } = new();

        public override long Result1()
        {
            for (int turn = 0; turn < 100; turn++)
            {
                foreach (var cell in this.Cells)
                {
                    cell.ProcessTurn();
                }

                foreach (var cell in this.Cells)
                {
                    cell.ProcessForFlashes();
                }

                if (this.PrintCells)
                {
                    PrintCellState($"After turn {turn + 1}", this.Cells, this.Size, this.Counter.Flashes);
                }
            }

            long result = this.Counter.Flashes;
            return result;
        }

        public override long Result2()
        {
            // Reset the data
            this.Cells = this.PrepData();

            int i = 1;
            while (true)
            {
                foreach (var cell in this.Cells)
                {
                    cell.ProcessTurn();
                }

                foreach (var cell in this.Cells)
                {
                    cell.ProcessForFlashes();
                }

                if (this.Cells.Flatten<Cell>().All(x => x.FlashedThisTurn))
                {
                    break;
                }

                i++;
            }

            long result = i;
            return result;
        }

        private static void PrintCellState(string description, Cell[,] cells, int size, int runningFlashCount)
        {
            int flashes = 0;
            Console.WriteLine(description);
            for (int y = 0; y < size; y++)
            {
                StringBuilder sb = new();
                for (int x = 0; x < size; x++)
                {
                    int value = cells[y, x].Value;
                    string s = value <= 9 ? value.ToString() : "*";
                    if (s == "*")
                    {
                        flashes++;
                    }

                    sb.Append(s);
                }

                Console.WriteLine(sb);
            }

            Console.WriteLine($"Flash count this turn: {flashes}. Cumulative: {runningFlashCount}");
        }

        private Cell[,] PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());

            List<Size> neighborLocations = new()
            {
                new Size(-1, -1),
                new Size(0, -1),
                new Size(1, -1),
                new Size(-1, 0),
                new Size(1, 0),
                new Size(-1, 1),
                new Size(0, 1),
                new Size(1, 1),
            };

            int size = lines.Length;

            Cell[,] cells = new Cell[size, size];

            for (int y = 0; y < size; y++)
            {
                char[] octopi = lines[y].ToCharArray();
                for (int x = 0; x < size; x++)
                {
                    cells[y, x] = new Cell(x, y, octopi[x] - '0');
                }
            }

            if (this.PrintCells)
            {
                PrintCellState("Initial State", cells, size, this.Counter.Flashes);
            }

            foreach (var cell in cells)
            {
                cell.Subscribe(this.Counter);
                var neighborCells = neighborLocations.Where(n =>
                {
                    Point p = new(cell.X, cell.Y);
                    Point newPoint = Point.Add(p, n);
                    return newPoint.X >= 0 && newPoint.X < size && newPoint.Y >= 0 && newPoint.Y < size;
                }).Select(n =>
                {
                    Point p = new(cell.X, cell.Y);
                    Point newPoint = Point.Add(p, n);
                    return cells[newPoint.Y, newPoint.X];
                }).ToArray();

                foreach (var neighbor in neighborCells)
                {
                    cell.Subscribe(neighbor);
                }
            }

            return cells;
        }
    }
}

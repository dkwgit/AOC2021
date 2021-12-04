// -----------------------------------------------------------------------
// <copyright file="BingoBoard.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Bingo
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;

    /// <summary>
    /// Domain model object for Day 4 of AOC 2021. A bingo board tracks announced numbers, wins, etc.
    /// </summary>
    public class BingoBoard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BingoBoard"/> class.
        /// </summary>
        /// <param name="board">Initialization data for the board, holding the numbers that this board has.</param>
        public BingoBoard(int[,] board)
        {
            int rows = board.GetUpperBound(0) + 1;
            int columns = board.GetUpperBound(1) + 1;
            rows.Should().Be(columns);
            this.BoardLength = rows;

            this.Board = new BoardPosition[this.BoardLength, this.BoardLength];
            this.Index = new BoardIndex();

            for (int row = 0; row < this.BoardLength; row++)
            {
                for (int column = 0; column < this.BoardLength; column++)
                {
                    int number = board[row, column];
                    this.Board[row, column] = new BoardPosition(row, column, number, false);
                    this.Index.Add(number, (row, column));
                }
            }
        }

        /// <summary>
        /// Gets or sets the winning number, which when announced led to the win on this board. -1 means no win.
        /// </summary>
        public int WinningNumber { get; set; } = -1;

        /// <summary>
        /// Gets or sets the most recent chronological visit number.
        ///
        /// This number is unique and helps track a chronology of the game as the list of all bingo numbers is sequentially
        /// announced to board after board (if a board has not one yet).
        /// For each such visit the board in question is given a unique board number. The very next board visited will
        /// get a number that is one higher, etc.
        ///
        /// Thus, VisitNumber helps us calculate the result that AOC2021 wants for problem 2 of Day 4, because it helps us find the very last board that had a win.
        /// </summary
        public int VisitNumber { get; set; } = 0;

        private BoardPosition[,] Board { get; init; }

        private BoardIndex Index { get; init; }

        private int BoardLength { get; init; }

        private int LastAnnounced { get; set; } = -1;

        /// <summary>
        /// Announced a new bingo number to a board and check for a win.
        /// </summary>
        /// <param name="number">The bingo number being announced.</param>
        /// <param name="visitNumber">The unique chronological visit number. Every visit to every board has a unique number during the game, providing a chronology.</param>
        /// <returns>Where there is a win or not.</returns>
        public bool AnnounceNumberAndCheck(int number, int visitNumber)
        {
            if (this.WinningNumber != -1)
            {
                return true;
            }

            this.VisitNumber = visitNumber;
            this.LastAnnounced = number;
            bool win = false;
            (int, int)? coordinate = this.Index.Find(number);
            if (coordinate.HasValue)
            {
                BoardPosition position = this.Board[coordinate.Value.Item1, coordinate.Value.Item2];
                position.Number.Should().Be(number);
                position.Announced = true;
                if (this.CheckForWin(coordinate.Value))
                {
                    this.WinningNumber = number;
                    win = true;
                }
            }

            return win;
        }

        /// <summary>
        /// Calculate problem solution result for Day 4 problem 2.
        /// This is consists of the sum all non-announced numbers * the winning number (the number that triggered a win for this board).
        /// </summary>
        /// <returns>The calculated result to check in AOC for a correct solution.</returns>
        public int CalculateWinResult()
        {
            int sumOfUnAnnounced = (
                from row in Enumerable.Range(0, this.BoardLength)
                from column in Enumerable.Range(0, this.BoardLength)
                where !this.Board[row, column].Announced
                select this.Board[row, column].Number).Sum();

            return sumOfUnAnnounced * this.WinningNumber;
        }

        private bool CheckForWin((int Row, int Column) coordinate)
        {
            int rowCheck = (from column in Enumerable.Range(0, this.BoardLength)
                            where this.Board[coordinate.Row, column].Announced
                            select column).Count();

            int columnCheck = (from row in Enumerable.Range(0, this.BoardLength)
                            where this.Board[row, coordinate.Column].Announced
                            select row).Count();

            bool win = (rowCheck == this.BoardLength) || (columnCheck == this.BoardLength);
            return win;
        }

        internal class BoardIndex
        {
            internal Dictionary<int, (int Row, int Column)> Index { get; init; } = new Dictionary<int, (int, int)>();

            internal void Add(int number, (int Row, int Column) coordinate)
            {
                this.Index[number] = coordinate;
            }

            internal (int X, int Y)? Find(int number)
            {
                (int, int)? coordinate = null;
                if (this.Index.ContainsKey(number))
                {
                    coordinate = this.Index[number];
                }

                return coordinate;
            }
        }

        internal class BoardPosition
        {
            internal BoardPosition(int row, int column, int number, bool announced) => (this.Row, this.Column, this.Number, this.Announced) = (row, column, number, announced);

            internal int Row { get; init; }

            internal int Column { get; init; }

            internal int Number { get; init; }

            internal bool Announced { get; set; }
        }
    }
}
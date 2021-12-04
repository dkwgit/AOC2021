using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace AOC2021
{
    public class BingoBoard
    {
        public BingoBoard(int id, int [,] board)
        {
            Id = id;

            int rows = board.GetUpperBound(0) + 1;
            int columns = board.GetUpperBound(1) + 1;
            rows.Should().Be(columns);
            BoardLength = rows;

            Board = new BoardPosition[BoardLength, BoardLength];
            Index = new BoardIndex();

            for(int row = 0; row < BoardLength; row++)
            {
                for (int column = 0; column < BoardLength; column++)
                {
                    int number = board[row, column];
                    Board[row, column] = new BoardPosition(row, column, number, false);
                    Index.Add(number, (row, column));
                }
            }
        }

        private BoardPosition [,] Board { get; init; }

        private BoardIndex Index { get; init; }

        private int BoardLength { get; init; }

        public int WinningNumber { get; set; } = -1;

        private int LastAnnounced { get; set; } = -1;

        public int Id { get; init; }

        public int VisitNumber { get; set; } = 0;

        public int VisitRound { get; set; } = 0;

        public bool AnnounceNumberAndCheck(int number, int visitRound, int visitNumber)
        {
            if (WinningNumber != -1)
            {
                return true;
            }
            VisitRound = visitRound;
            VisitNumber = visitNumber;
            LastAnnounced = number;
            bool win = false;
            (int, int)? coordinate = Index.Find(number);
            if (coordinate.HasValue)
            {
                BoardPosition position = Board[coordinate.Value.Item1, coordinate.Value.Item2];
                position.Number.Should().Be(number);
                position.Announced = true;
                if (CheckForWin(coordinate.Value))
                {
                    WinningNumber = number;
                    win = true;
                }
            }

            return win;
        }

        public int CalculateWinResult()
        {
           int sumOfUnAnnounced = (from row in Enumerable.Range(0, BoardLength)
             from column in Enumerable.Range(0, BoardLength)
             where !Board[row, column].Announced
             select Board[row, column].Number).Sum();

            return sumOfUnAnnounced * WinningNumber;
        }

        private bool CheckForWin((int, int) coordinate)
        {
            int rowCheck = (from column in Enumerable.Range(0, BoardLength)
                            where Board[coordinate.Item1, column].Announced
                            select column).Count();

            int columnCheck = (from row in Enumerable.Range(0, BoardLength)
                            where Board[row, coordinate.Item2].Announced
                            select row).Count();

            bool win = (rowCheck == BoardLength || columnCheck == BoardLength);
            return win;
        }

        internal class BoardIndex
        {
            internal  Dictionary<int, (int, int)> Index { get; init; } = new Dictionary<int, (int, int)>();

            internal void Add(int number, (int, int) coordinate)
            {
                Index[number] = coordinate;
            }

            internal (int x, int y)? Find(int number)
            {
                (int, int)? coordinate = null;
                if (Index.ContainsKey(number))
                {
                    coordinate = Index[number];
                }
                return coordinate;
            }
        }
    }

    internal class BoardPosition
    {
        internal BoardPosition(int row, int column, int number, bool announced) => (Row, Column, Number, Announced) = (row, column, number, announced);

        internal int Row { get; init; }

        internal int Column { get; init; }

        internal int Number { get; init; }
        internal bool Announced { get; set; }
    }
}
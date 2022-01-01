// -----------------------------------------------------------------------
// <copyright file="Image.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.ImageEnhancement
{
    using System.Collections;
    using System.Text;

    internal class Image
    {
        private int turn = 0;

        private char[,] data;

        private ImageCursor? cursor;

        internal Image(int rows, int columns, int padding, char[] enhancementTable)
        {
            this.UnpaddedRows = rows;
            this.UnpaddedColumns = columns;
            this.Rows = rows + (2 * padding);
            this.Columns = columns + (2 * padding);
            this.Padding = padding;
            this.data = new char[this.Rows, this.Columns];
            for (int row = 0; row < this.Rows; row++)
            {
                for (int column = 0; column < this.Columns; column++)
                {
                    this.data[row, column] = '.';
                }
            }

            this.EnhancementTable = enhancementTable;
        }

        public char[] EnhancementTable { get; }

        internal int Rows { get; }

        internal int Columns { get; }

        internal int UnpaddedRows { get; }

        internal int UnpaddedColumns { get; }

        internal int Padding { get; }

        internal int Turn
        {
            get
            {
                return this.turn;
            }
        }

        internal ImageCursor Cursor
        {
            get
            {
                if (this.cursor == null)
                {
                    throw new InvalidOperationException("Use of Cursor property before initialized");
                }

                return this.cursor;
            }
        }

        internal ImageCursor GetCursor()
        {
            ImageCursor cursor = new(
                upperRow: this.Padding - this.Turn - 2,
                lowerRow: this.Padding + this.UnpaddedRows + this.Turn - 1,
                leftColumn: this.Padding - this.Turn - 2,
                rightColumn: this.Padding + this.UnpaddedColumns + this.Turn - 1);

            return cursor;
        }

        internal void Set(int row, int column, char item)
        {
            this.data[row, column] = item;
        }

        internal char Get(int row, int column)
        {
            if (row < 0 || column < 0 || row >= this.Rows || column >= this.Columns)
            {
                throw new ArgumentOutOfRangeException($"Bad argument of either {row} or column {column} passed to GetWithoutPadding");
            }

            return this.data[row, column];
        }

        internal void Enhance()
        {
            this.cursor = this.GetCursor();

            char[,] newData = new char[this.data.GetLength(0), this.data.GetLength(1)];
            for (int row = 0; row < this.Rows; row++)
            {
                for (int column = 0; column < this.Columns; column++)
                {
                    newData[row, column] = '.';
                }
            }

            while (true)
            {
                this.Cursor.Fill(this);
                (int row, int column) = this.Cursor.Position;
                int bitValue = this.Cursor.BitValue;
                char value = this.EnhancementTable[bitValue];
                newData[row, column] = value;
                if (this.Cursor.CanMoveRight)
                {
                    this.Cursor.MoveRight();
                }
                else if (this.Cursor.CanMoveDown)
                {
                    this.Cursor.CarriageReturn();
                }
                else
                {
                    break;
                }
            }

            this.data = newData;
            if (this.Turn % 2 == 0)
            {
                this.SetEdge('#');
            }
            else
            {
                this.SetEdge('.');
            }

            this.turn++;
        }

        internal int CountOnPixels()
        {
            int on = 0;

            for (int row = 0; row < this.Rows; row++)
            {
                for (int column = 0; column < this.Columns; column++)
                {
                    if (this.Get(row, column) == '#')
                    {
                        on++;
                    }
                }
            }

            return on;
        }

        internal void SetEdge(char edgeChar)
        {
            /*
             * The 'Edge' is a 2 wide hollow rectangle, that, at the start of things, is 2 to 3 away from the loaded data.
             * This allows each turn to grow the data into the space 1 away from the data,
             * with the edge being the last bit of uniform infinity before we reach the data.
             *
             * We can use the cursor to caculate the edge, because we have the upper left corner of the cursor box start
             * two away from the upper left corner of the data.
             *
             * The final cursor 'Position', the center of the cursor box, not a corner. is 1 away from the data at the
             * lower right hand. (The cursor positions traverse the space one away from the data, the growth area, and
             * the data).
             *
             * Here is the starting box of the cursor, with E being edge area, G being Growth area and D being the upper
             * left of the data. Note that e is the starting upper left corner of the cursor box.
             * The g at the center is the first cursor position (where the calculation result of ths box is placed).
             *
             * EEEE
             * EeEE
             * EEgG
             * EEGD
             *
             * Here is the final position of the cursor with E being Edge area, G being growth area and D being the
             * lower right of the data. Note that the final 'Position' of the cursor is at g. The cursor does not
             * track e, but that is its box's lower right corner.
             *
             * DGEE
             * GgEE
             * EEeE
             * EEEE
             */

            int topEdgeRow = this.Cursor.UpperRow - 1;
            int leftEdgeColumn = this.Cursor.LeftColumn - 1;
            int bottomEdgeRow = this.Cursor.Position.Row + 2;
            int rightEdgeColumn = this.Cursor.Position.Column + 2;

            foreach (int row in new List<int> { topEdgeRow, topEdgeRow + 1, bottomEdgeRow - 1, bottomEdgeRow })
            {
                for (int column = leftEdgeColumn; column <= rightEdgeColumn; column++)
                {
                    this.Set(row, column, edgeChar);
                }
            }

            /*
             * We already handled bottom and top edge rows, now do the left and right columns,
             * minus the top and bottom most cells, already handled when we did the rows.
             */
            for (int row = topEdgeRow + 2; row <= bottomEdgeRow - 2; row++)
            {
                foreach (int column in new List<int> { leftEdgeColumn, leftEdgeColumn + 1, rightEdgeColumn - 1, rightEdgeColumn })
                {
                    this.Set(row, column, edgeChar);
                }
            }
        }

        internal void Print()
        {
            Console.WriteLine($"Turn {this.Turn}");
            for (int row = 0; row < this.Rows; row++)
            {
                StringBuilder sb = new();
                for (int column = 0; column < this.Columns; column++)
                {
                    sb.Append(this.Get(row, column));
                }

                Console.WriteLine(sb.ToString());
            }
        }
    }
}

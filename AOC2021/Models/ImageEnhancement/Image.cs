// -----------------------------------------------------------------------
// <copyright file="Image.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.ImageEnhancement
{
    using System.Collections;

    internal class Image
    {
        private int enhancementTurn = 0;

        private bool[,] data;

        internal Image(int rows, int columns, int padding, BitArray enhancementTable, bool initialValue)
        {
            this.UnpaddedRows = rows;
            this.UnpaddedColumns = columns;
            this.Rows = rows + (2 * padding);
            this.Columns = columns + (2 * padding);
            this.Padding = padding;
            this.data = new bool[this.Rows, this.Columns];
            if (initialValue)
            {
                for (int row = 0; row < this.Rows; row++)
                {
                    for (int column = 0; column < this.Columns; column++)
                    {
                        this.data[row, column] = initialValue;
                    }
                }
            }

            this.EnhancementTable = enhancementTable;
        }

        public BitArray EnhancementTable { get; } = new(512);

        internal int Rows { get; }

        internal int Columns { get; }

        internal int UnpaddedRows { get; }

        internal int UnpaddedColumns { get; }

        internal int Padding { get; }

        internal int EnhancementTurn
        {
            get
            {
                return this.enhancementTurn;
            }
        }

        internal ImageCursor GetCursor()
        {
            return new ImageCursor(0, this.Padding + this.UnpaddedRows + this.Padding - 3, 0, this.Padding + this.UnpaddedColumns + this.Padding - 3);
        }

        internal void Set(int row, int column, bool set)
        {
            this.data[row, column] = set;
        }

        internal bool Get(int row, int column)
        {
            if (row < 0 || column < 0 || row >= this.Rows || column >= this.Columns)
            {
                throw new ArgumentOutOfRangeException($"Bad argument of either {row} or column {column} passed to GetWithoutPadding");
            }

            return this.data[row, column];
        }

        internal void Enhance()
        {
            ImageCursor cursor = this.GetCursor();

            bool[,] newData = new bool[this.data.GetLength(0), this.data.GetLength(1)];

            while (true)
            {
                cursor.Fill(this);
                (int row, int column) = cursor.Position;
                int bitValue = cursor.BitValue;
                bool value = this.EnhancementTable[bitValue];
                newData[row, column] = value;
                if (cursor.CanMoveRight)
                {
                    cursor.MoveRight();
                }
                else if (cursor.CanMoveDown)
                {
                    cursor.CarriageReturn();
                }
                else
                {
                    break;
                }
            }

            this.data = newData;
            this.enhancementTurn++;
        }

        internal int CountOnPixels()
        {
            int on = 0;

            for (int row = 0; row < this.Rows; row++)
            {
                for (int column = 0; column < this.Columns; column++)
                {
                    if (this.Get(row, column))
                    {
                        on++;
                    }
                }
            }

            return on;
        }

        internal void FlipEdges()
        {
            foreach (int row in new List<int> { 0, this.Rows - 1 })
            {
                for (int column = 0; column < this.Columns; column++)
                {
                    this.Set(row, column, !this.Get(row, column));
                }
            }

            for (int row = 1; row < this.Rows - 1; row++)
            {
                foreach (int column in new List<int> { 0, this.Columns - 1 })
                {
                    this.Set(row, column, !this.Get(row, column));
                }
            }
        }
    }
}

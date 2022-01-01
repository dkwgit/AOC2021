// -----------------------------------------------------------------------
// <copyright file="ImageCursor.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.ImageEnhancement
{
    internal class ImageCursor
    {
        private int currentRow = 0;

        private int currentColumn = 0;

        internal ImageCursor(int upperRow, int lowerRow, int leftColumn, int rightColumn)
        {
            this.UpperRow = upperRow;
            this.LowerRow = lowerRow;
            this.LeftColumn = leftColumn;
            this.RightColumn = rightColumn;
            this.currentRow = upperRow;
            this.currentColumn = leftColumn;
            this.Erase();
        }

        internal char[,] Data { get; } = new char[3, 3];

        internal int UpperRow { get; }

        internal int LowerRow { get; }

        internal int LeftColumn { get; }

        internal int RightColumn { get; }

        internal int CurrentRow
        {
            get
            {
                return this.currentRow;
            }
        }

        internal int CurrentColumn
        {
            get
            {
                return this.currentColumn;
            }
        }

        internal (int Row, int Column) Position
        {
            get
            {
                return (this.CurrentRow + 1, this.currentColumn + 1);
            }
        }

        internal int BitValue
        {
            get
            {
                int bitValue = 0;

                for (int row = 0; row < 3; row++)
                {
                    for (int column = 0; column < 3; column++)
                    {
                        bitValue |= this.Data[row, column] == '#' ? 1 : 0;
                        if (row != 2)
                        {
                            bitValue <<= 1;
                        }
                        else if (column != 2)
                        {
                            bitValue <<= 1;
                        }
                    }
                }

                return bitValue;
            }
        }

        internal bool CanMoveRight
        {
            get
            {
                if (this.CurrentColumn >= this.RightColumn)
                {
                    return false;
                }

                return true;
            }
        }

        internal bool CanMoveDown
        {
            get
            {
                if (this.CurrentRow >= this.LowerRow)
                {
                    return false;
                }

                return true;
            }
        }

        internal void Fill(Image image)
        {
            for (int row = this.CurrentRow; row < this.CurrentRow + 3; row++)
            {
                for (int column = this.CurrentColumn;  column < this.CurrentColumn + 3; column++)
                {
                    this.Data[row - this.CurrentRow, column - this.CurrentColumn] = image.Get(row, column);
                }
            }
        }

        internal void MoveRight()
        {
            if (!this.CanMoveRight)
            {
                throw new InvalidOperationException("Cannot move right");
            }

            this.currentColumn++;
            this.Erase();
        }

        internal void MoveDown()
        {
            if (!this.CanMoveDown)
            {
                throw new InvalidOperationException("Cannot move down");
            }

            this.currentRow++;
            this.Erase();
        }

        internal void CarriageReturn()
        {
            if (!this.CanMoveDown)
            {
                throw new InvalidOperationException("Cannot move down");
            }

            this.currentColumn = this.LeftColumn;
            this.currentRow++;
            this.Erase();
        }

        internal void Erase()
        {
            this.Data[0, 0] = '.';
            this.Data[0, 1] = '.';
            this.Data[0, 2] = '.';
            this.Data[1, 0] = '.';
            this.Data[1, 1] = '.';
            this.Data[1, 2] = '.';
            this.Data[2, 0] = '.';
            this.Data[2, 1] = '.';
            this.Data[2, 2] = '.';
        }
    }
}

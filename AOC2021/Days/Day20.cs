// -----------------------------------------------------------------------
// <copyright file="Day20.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Collections;
    using AOC2021.Data;
    using AOC2021.Models.ImageEnhancement;
    using FluentAssertions;

    internal class Day20 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        private Image? image;

        public Day20(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public Image Image
        {
            get
            {
                if (this.image == null)
                {
                    throw new InvalidOperationException("Image property accessed before initialized");
                }

                return this.image;
            }
        }

        public override string GetDescription()
        {
            return "Image enhancement";
        }

        public override string Result1()
        {
            this.PrepData();

            this.Image.Enhance();
            this.Image.FlipEdges();
            Image image = new(this.Image.UnpaddedRows + 6, this.Image.UnpaddedColumns + 6, this.Image.Padding, this.Image.EnhancementTable, true);
            for (int row = 0; row < this.Image.Rows; row++)
            {
                for (int column = 0; column < this.Image.Columns; column++)
                {
                    bool value = this.Image.Get(row, column);
                    image.Set(row + 3, column + 3, value);
                }
            }

            this.image = image;
            this.Image.Enhance();

            long result = this.Image.CountOnPixels();
            return result.ToString();
        }

        public override string Result2()
        {
            long result = 0;
            return result.ToString();
        }

        internal void PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());
            lines[0].Length.Should().Be(512);

            BitArray enhancementTable = new(512);
            for (int i = 0; i < lines[0].Length; i++)
            {
                enhancementTable[i] = lines[0][i] == '#';
            }

            string[] imageLines = lines[2..];
            int lineCount = imageLines.Length;

            this.image = new Image(lineCount, lineCount, 3, enhancementTable, false);

            int row = 0;
            foreach (string line in imageLines)
            {
                for (int column = 0; column < line.Length; column++)
                {
                    this.Image.Set(row + 3, column + 3, line[column] == '#');
                }

                row++;
            }
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="Day03.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Collections.Specialized;
    using AOC2021.Data;
    using FluentAssertions;

    /// <summary>
    /// Day 3: Reading the diagnostic computer.
    /// </summary>
    public class Day03 : IDay
    {
        private readonly DataStore datastore;

        public Day03(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public string Name { get; init; } = "Day03";

        public long Result1()
        {
            (BitVector32[] diagRows, int diagColumnCount) = this.PrepData();
            int diagRowCount = diagRows.Length;

            BitVector32 epsilon = new(0);
            int[] columnSums = new int[diagColumnCount];

            int mask;
            for (int r = 0; r < diagRows.Length; r++)
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

            long result = epsilonAsInt * gammaAsInt;
            return result;
        }

        public long Result2()
        {
            (BitVector32[] diags, int columnCount) = this.PrepData();

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

            long result = oxygenNumber * scrubberNumber;
            return result;
        }

        private (BitVector32[] DiagRows, int DiagColumns) PrepData()
        {
            string[] data = this.datastore.GetRawData(this.Name);
            int columnCount = -1;
            var diagRows = data.Select(s =>
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
            }).ToArray();

            return (diagRows, columnCount);
        }
    }
}

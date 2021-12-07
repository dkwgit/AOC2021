// -----------------------------------------------------------------------
// <copyright file="Sea.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models
{
    internal class Sea
    {
        private readonly int slots;

        private readonly List<long> generations;

        internal Sea(int slots, int[] starterFish)
        {
            this.slots = slots;
            this.generations = new List<long>(new long[this.slots]);
            var discard = starterFish.Select((int d) =>
            {
                long value = this.generations[d];
                value++;
                this.generations[d] = value;
                return d;
            }).Count();
        }

        internal long TotalFish
        {
            get
            {
                return this.generations.Sum(g => g);
            }
        }

        internal void ADayAtSea()
        {
            // Add an extra slot at the end, since we will be dropping the slot at the front.
            // It has index = 9. It will be become index = 8.
            this.generations.Add(0L);

            // Add this many fish to what will become index = 8;
            // And also to what will become index = 6
            this.generations[this.slots] = this.generations[0];
            this.generations[7] += this.generations[0];

            // Drop the 0 slot, which means all the other fishies get gets -1 to their generation.
            this.generations.RemoveAt(0);
        }
    }
}
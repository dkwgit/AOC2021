// -----------------------------------------------------------------------
// <copyright file="Day18.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;
    using AOC2021.Models.SnailFish;

    public class Day18 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        private SnailFishNumber? currentNumber;

        private List<string>? otherNumbers;

        public Day18(DataStore datastore)
        {
            this.datastore = datastore;
        }

        internal SnailFishNumber CurrentNumber
        {
            get
            {
                if (this.currentNumber == null)
                {
                    throw new InvalidOperationException("Dereference of currentNumber before initialized");
                }

                return this.currentNumber;
            }
        }

        internal List<string> OtherNumbers
        {
            get
            {
                if (this.otherNumbers == null)
                {
                    throw new InvalidOperationException("Dereference of currentNumber before initialized");
                }

                return this.otherNumbers;
            }
        }

        public override string GetDescription()
        {
            return "Snailfish math homework";
        }

        public override string Result1()
        {
            this.PrepData();
            foreach (var number in this.OtherNumbers)
            {
                this.CurrentNumber.Add(number);
            }

            long result = this.CurrentNumber.GetMagnitude();
            return result.ToString();
        }

        public override string Result2()
        {
            this.PrepData();
            this.OtherNumbers.Insert(0, this.CurrentNumber.Number);

            int largestMangnitude = int.MinValue;
            for (int i = 0; i < this.OtherNumbers.Count; i++)
            {
                for (int j = 0; j < this.OtherNumbers.Count; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    SnailFishNumber first = new(this.OtherNumbers[i]);
                    first.Add(this.OtherNumbers[j]);
                    int magnitude = first.GetMagnitude();
                    if (magnitude > largestMangnitude)
                    {
                        largestMangnitude = magnitude;
                    }
                }
            }

            long result = largestMangnitude;
            return result.ToString();
        }

        internal void PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());
            this.currentNumber = new SnailFishNumber(lines[0]);
            this.otherNumbers = new List<string>(lines[1..]);
        }
    }
}

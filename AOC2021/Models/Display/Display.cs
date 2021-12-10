// -----------------------------------------------------------------------
// <copyright file="Display.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    using System.Linq;

    internal class Display
    {
        internal List<IDigit> Digits { get; } = new();

        internal int Solve(string[] outputs)
        {
            this.
                GetUnknownsDigitsBySignalLength(5).
                FindSegmentIntersectionWith(this.GetDigit(1)).
                ToProduce(3).
                Then().
                GetUnknownsDigitsBySignalLength(6).
                FindSegmentIntersectionWith(this.GetDigit(4)).
                ToProduce(9).
                Then().
                GetUnknownsDigitsBySignalLength(6).
                FindSegmentIntersectionWith(this.GetDigit(1)).
                ToProduce(0).
                Then().
                GetUnknownsDigitsBySignalLength(6).
                Single().
                ToProduce(6).
                Then().
                GetUnknownsDigitsBySignalLength(5).
                // 1 shares exactly the same segment with 6 as with 5, which we are looking for
                FindDigitBySingleSegment(this.GetDigit(1).IntersectSegments(this.GetDigit(6)).Single()).
                ToProduce(5).
                Then().
                GetUnknownsDigitsBySignalLength(5).
                Single().
                ToProduce(2);

            int factor = 1000;
            int outputNumber = 0;
            foreach (var output in outputs)
            {
                string sorted = output.SortString();
                outputNumber += this.Digits.Where(x => x.Signal == sorted).Select(x => x.Digit * factor).Single();
                factor /= 10;
            }

            return outputNumber;
        }

        internal void AddSignal(string signal)
        {
            IDigit digit = signal.Length switch
            {
                2 => new Digit(this, signal, 1),
                3 => new Digit(this, signal, 7),
                4 => new Digit(this, signal, 4),
                5 => new Unknown(this, signal),
                6 => new Unknown(this, signal),
                7 => new Digit(this, signal, 8),
                _ => throw new InvalidDataException(),
            };

            this.Digits.Add(digit);
        }

        internal IDigit GetDigit(int i)
        {
            return this.Digits.Where(d => d.Digit == i).Single();
        }

        internal Display Then()
        {
            return this;
        }

        internal IEnumerable<(IDigit D, int Index)> GetUnknownsDigitsBySignalLength(int length)
        {
            return this.Digits.Select((d, index) => (d, index)).Where(x => x.d.Digit == -1 && x.d.Length == length);
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="Digit.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    using System.Linq;

    internal class Digit : IDigit
    {
        private readonly Display display;

        private readonly string signal;

        private readonly int digit;

        private readonly char[] segments;

        private readonly int length;

        internal Digit(Display display, string signal, int digit)
        {
            signal = signal.SortString();

            this.display = display;

            this.segments = signal.ToCharArray();

            this.signal = signal;

            this.digit = digit;

            this.length = signal.Length;
        }

        public string Signal => this.signal;

        public Display Display => this.display;

        public int Length => this.length;

        public char[] Segments => this.segments;

        int IDigit.Digit => this.digit;

        internal IEnumerable<IDigit> IntersectsUnknowns()
        {
            return this.display.Digits.Where(d => d.Digit == -1);
        }
    }
}

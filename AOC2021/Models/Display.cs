// -----------------------------------------------------------------------
// <copyright file="Display.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models
{
    using System.Linq;

    internal enum SegmentId
    {
        Segment0, // aaaa
        Segment1, // bb
        Segment2, // cc
        Segment3, // dddd
        Segment4, // ee
        Segment5, // ff
        Segment6, // gggg
    }

    internal class Display
    {
        internal Display()
        {
            for (int i = 0; i < 10; i++)
            {
                this.Wirings[i] = new DigitWiring(i, this.Digit2SegmentLength[i], string.Empty, new SortedSet<char>());
            }

            for (int j = 0; j < 7; j++)
            {
                this.SegmentCandidates.Add(this.SegmentNames[j], new SortedSet<int>());
            }
        }

        internal int[] Digit2SegmentLength { get; } = new int[10] { 6, 2, 5, 5, 4, 5, 6, 3, 7, 6 };

        internal Dictionary<int, SortedSet<int>> SegmentCountToDigits { get; } = new()
        {
            { 2, new SortedSet<int>() { 1 } },
            { 3, new SortedSet<int>() { 7 } },
            { 4, new SortedSet<int>() { 4 } },
            { 5, new SortedSet<int>() { 2, 3, 5 } },
            { 6, new SortedSet<int>() { 0, 6, 9 } },
            { 7, new SortedSet<int>() { 8 } },
        };

        internal char[] SegmentNames { get; } = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

        private DigitWiring[] Wirings { get; } = new DigitWiring[10];

        private Dictionary<char, SortedSet<int>> SegmentCandidates { get; } = new();

        private Dictionary<string, SortedSet<int>> PatternCandidates { get; set; } = new();

        internal void AddPattern(string pattern)
        {
            pattern = (pattern == null) ? string.Empty : pattern;

            char[] segments = pattern.ToCharArray().OrderBy(c => c).ToArray();
            SortedSet<int> digitCandidateSet = this.SegmentCountToDigits[segments.Length];

            this.PatternCandidates[pattern] = digitCandidateSet;

            foreach (char segment in segments)
            {
                var set = this.SegmentCandidates[segment];
                var digitList = this.SegmentCountToDigits[segments.Length];
                foreach (int digit in digitList)
                {
                    set.Add(digit);
                }
            }

            if (digitCandidateSet.Count == 1)
            {
                int digit = digitCandidateSet.First<int>();
                for (int i = 0; i < segments.Length; i++)
                {
                    this.Wirings[digit].Segments.Add(segments[i]);
                }
            }
        }

        internal void Solve()
        {
            bool search = true;

            while (search)
            {
                foreach (var pattern in this.PatternCandidates)
                {
                    if ()
                }
            }
        }
    }

    internal record DigitWiring(int Digit, int SegmentCount, SortedSet<char> Segments)
    {
        internal string Pattern { get; set; } = string.Empty;
    }
}

// -----------------------------------------------------------------------
// <copyright file="Day10.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Collections.Generic;
    using AOC2021.Data;
    using FluentAssertions;

    /// <summary>
    /// Day 10: Can we get some closure in here.
    /// </summary>
    public class Day10 : IDay
    {
        private readonly DataStore datastore;

        public Day10(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public string Name { get; init; } = "Day10";

        private List<string> ValidLines { get; } = new();

        private Dictionary<char, char> ClosePairs { get; } = new() { { ')', '(' }, { ']', '[' }, { '}', '{' }, { '>', '<' } };

        private Dictionary<char, char> OpenPairs { get; } = new() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };

        public long Result1()
        {
            string[] lines = this.PrepData();
            List<char> corrupted = new();

            foreach (string line in lines)
            {
                bool badLine = false;
                char[] tokens = line.ToCharArray();

                Stack<char> stack = new();
                foreach (char current in tokens)
                {
                    if (!this.ClosePairs.ContainsKey(current))
                    {
                        stack.Push(current);
                    }
                    else
                    {
                        char stackTop = stack.Peek();
                        if (this.ClosePairs[current] != stackTop)
                        {
                            corrupted.Add(current);
                            badLine = true;
                            break;
                        }
                        else
                        {
                            stack.Pop();
                        }
                    }
                }

                if (!badLine)
                {
                    this.ValidLines.Add(line);
                }
            }

            long result = 0;
            foreach (char corrupt in corrupted)
            {
                result += corrupt switch
                {
                    ')' => 3,
                    ']' => 57,
                    '}' => 1197,
                    '>' => 25137,
                    _ => throw new InvalidDataException("Unexpected character in illegal list."),
                };
            }

            return result;
        }

        public long Result2()
        {
            var lines = this.ValidLines;
            List<List<char>> allLineFixes = new();

            foreach (string line in lines)
            {
                char[] tokens = line.ToCharArray();
                Stack<char> unresolvedOpens = new();

                foreach (char current in tokens)
                {
                    if (this.OpenPairs.ContainsKey(current))
                    {
                        unresolvedOpens.Push(current);
                    }

                    if (this.ClosePairs.ContainsKey(current))
                    {
                        char stackTop = unresolvedOpens.Pop();
                        stackTop.Should().Be(this.ClosePairs[current]);
                    }
                }

                List<char> lineFixes = new();
                while (unresolvedOpens.Count > 0)
                {
                    char last = unresolvedOpens.Pop();
                    lineFixes.Add(this.OpenPairs[last]);
                }

                allLineFixes.Add(lineFixes);
            }

            List<long> scores = new();
            foreach (var oneLineFix in allLineFixes)
            {
                long score = 0;
                foreach (char oneFix in oneLineFix)
                {
                    score *= 5;
                    score += oneFix switch
                    {
                        ')' => 1,
                        ']' => 2,
                        '}' => 3,
                        '>' => 4,
                        _ => throw new InvalidDataException("Unexpected character in illegal list."),
                    };
                }

                scores.Add(score);
            }

            scores = scores.OrderBy(x => x).ToList();
            int remainder = scores.Count % 2;
            remainder.Should().Be(1);

            // integer division rounds down, but that works with 0 based array index to find middle
            long result = scores[scores.Count / 2];
            return result;
        }

        private string[] PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.Name);

            return lines;
        }
    }
}

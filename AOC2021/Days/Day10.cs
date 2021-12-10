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
    public class Day10 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day10(DataStore datastore)
        {
            this.datastore = datastore;
        }

        // For a non corrupt line, the stack of opening elements that did not get resolved
        private List<Stack<char>> UnresolvedOpens { get; } = new();

        private Dictionary<char, char> CloseToOpen { get; } = new() { { ')', '(' }, { ']', '[' }, { '}', '{' }, { '>', '<' } };

        private Dictionary<char, char> OpenToClose { get; } = new() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };

        public override long Result1()
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
                    if (!this.CloseToOpen.ContainsKey(current))
                    {
                        stack.Push(current);
                    }
                    else
                    {
                        char stackTop = stack.Pop();
                        if (this.CloseToOpen[current] != stackTop)
                        {
                            corrupted.Add(current);
                            badLine = true;
                            break;
                        }
                    }
                }

                if (!badLine)
                {
                    this.UnresolvedOpens.Add(stack);
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
                    _ => throw new InvalidDataException("Unexpected character in corrupt list."),
                };
            }

            return result;
        }

        public override long Result2()
        {
            List<List<char>> allLineFixes = new(this.UnresolvedOpens.Count);

            foreach (Stack<char> stack in this.UnresolvedOpens)
            {
                List<char> lineFixes = new(stack.Count);
                while (stack.Count > 0)
                {
                    char top = stack.Pop();
                    lineFixes.Add(this.OpenToClose[top]);
                }

                allLineFixes.Add(lineFixes);
            }

            long[] scores = this.GetSortedScores(allLineFixes);
            int remainder = scores.Length % 2;
            remainder.Should().Be(1);

            // integer division rounds down, but that works with 0 based array index to find middle
            long result = scores[scores.Length / 2];
            return result;
        }

        private long[] GetSortedScores(List<List<char>> allLineFixes)
        {
            List<long> scores = new(allLineFixes.Count);
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
                        _ => throw new InvalidDataException("Unexpected character in fix list."),
                    };
                }

                scores.Add(score);
            }

            return scores.OrderBy(x => x).ToArray();
        }

        private string[] PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());

            return lines;
        }
    }
}

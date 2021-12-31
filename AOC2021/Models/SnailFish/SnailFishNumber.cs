// -----------------------------------------------------------------------
// <copyright file="SnailFishNumber.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.SnailFish
{
    using System.Text.RegularExpressions;
    using FluentAssertions;

    internal enum ReductionType
    {
        Explode,
        Split,
        None,
    }

    internal class SnailFishNumber
    {
        private string number = string.Empty;

        internal SnailFishNumber(string number)
        {
            this.number = number;
        }

        internal string Number => this.number;

        internal ((int Left, int Right) Indices, ReductionType Reduction) FindReduce()
        {
            ReductionType reduction = ReductionType.None;
            int left = -1;
            int right = -1;

            char[] chars = this.number.ToCharArray();

            bool explode = false;
            bool lastWasDigit = false;
            int splitCandidate = -1;
            Stack<int> openBracketPositions = new();

            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsDigit(chars[i]))
                {
                    if (lastWasDigit && splitCandidate == -1)
                    {
                        splitCandidate = i - 1;
                    }
                    else
                    {
                        lastWasDigit = true;
                    }
                }
                else
                {
                    lastWasDigit = false;
                }

                if (chars[i] == '[')
                {
                    openBracketPositions.Push(i);
                    if (openBracketPositions.Count > 4)
                    {
                        left = i;
                        explode = true;
                    }
                }

                if (chars[i] == ']')
                {
                    if (explode)
                    {
                        right = i;
                        break;
                    }
                    else
                    {
                        openBracketPositions.Pop();
                    }
                }
            }

            if (explode)
            {
                reduction = ReductionType.Explode;
                left = openBracketPositions.Peek();
            }
            else if (splitCandidate != -1)
            {
                reduction = ReductionType.Split;
                left = splitCandidate;
            }

            return ((left, right), reduction);
        }

        internal string Explode((int Left, int Right) indices)
        {
            // The thing to replace with a "0"
            string sub = this.Number[indices.Left..(indices.Right + 1)];

            string pattern = @"\[(\d+),(\d+)\]";
            Match m = Regex.Match(sub, pattern);

            // stuff to left of the thing to replace
            string leftString = this.Number[..indices.Left];

            // stuff to right of the thing to replace
            string rightString = this.Number[(indices.Right + 1)..];

            // for locating the nearest numbers to the left and right, if any
            int nearestLeftNumberIndex = -1;
            int nearestRightNumberIndex = -1;
            string nearestLeftNumber = string.Empty;
            string nearestRightNumber = string.Empty;

            // Parse backward and find a possible nearest number to the left
            for (int i = leftString.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(leftString[i]))
                {
                    nearestLeftNumberIndex = i;
                    nearestLeftNumber = leftString[i].ToString();
                    if (i > 0)
                    {
                        if (char.IsDigit(leftString[i - 1]))
                        {
                            // this case is a multi-digit item
                            nearestLeftNumberIndex = i - 1;
                            nearestLeftNumber = leftString[i - 1].ToString() + nearestLeftNumber;
                        }

                        break;
                    }
                }
            }

            // Parse forward and find a possible nearest number to the right
            for (int i = 0; i < rightString.Length; i++)
            {
                if (char.IsDigit(rightString[i]))
                {
                    nearestRightNumberIndex = i;
                    nearestRightNumber = rightString[i].ToString();
                    if (i + i < rightString.Length)
                    {
                        if (char.IsDigit(rightString[i + 1]))
                        {
                            // this case is a multi-digit item
                            nearestRightNumber += rightString[i + 1].ToString();
                        }
                    }

                    break;
                }
            }

            if (m.Success)
            {
                // The left number of the pair being exploded
                string leftItem = m.Groups[1].Value;

                // The right number of the pair being exploded
                string rightItem = m.Groups[2].Value;
                if (nearestLeftNumberIndex != -1)
                {
                    // Add the left number of the pair to the nearst number to the left.
                    string a = leftString[..nearestLeftNumberIndex];
                    string b = leftString[(nearestLeftNumberIndex + nearestLeftNumber.Length)..];
                    leftString = a + (int.Parse(leftItem) + int.Parse(nearestLeftNumber)).ToString() + b;
                }

                if (nearestRightNumberIndex != -1)
                {
                    // Add the right number of the pair to the nearest number to the right.
                    string a = rightString[..nearestRightNumberIndex];
                    string b = rightString[(nearestRightNumberIndex + nearestRightNumber.Length)..];
                    rightString = a + (int.Parse(rightItem) + int.Parse(nearestRightNumber)).ToString() + b;
                }

                // Do the replacement
                return leftString + "0" + rightString;
            }
            else
            {
                throw new InvalidDataException($"Expected pattern {pattern}");
            }
        }

        internal string Split(int index)
        {
            string numberToSplit = this.Number[index..(index + 2)].ToString();

            int number = int.Parse(numberToSplit);
            int left;
            int right;
            if (number % 2 != 0)
            {
                left = number / 2;
                right = (number / 2) + 1;
            }
            else
            {
                left = right = number / 2;
            }

            return this.Number[..index] + $"[{left},{right}]" + this.Number[(index + 2)..];
        }

        internal int GetMagnitude()
        {
            UnresolvedPairItem unresolved = new();
            Pair top = new(unresolved.Increment(), unresolved.Increment());
            Stack<(IPair Pair, PairState State)> stack = new();

            IPair currentPair = top;
            PairState state = PairState.FillingLeft;

            // We start at one because we have already created the first pair to fill.
            for (int i = 1; i < this.Number.Length; i++)
            {
                if (this.Number[i] == '[')
                {
                    if (state == PairState.FillingLeft)
                    {
                        currentPair.Left = new Pair(unresolved.Increment(), unresolved.Increment());
                        unresolved.Decrement();
                        stack.Push((currentPair, PairState.FillingRight));
                        currentPair = (Pair)currentPair.Left;
                        state = PairState.FillingLeft;
                    }
                    else
                    {
                        currentPair.Right = new Pair(unresolved.Increment(), unresolved.Increment());
                        unresolved.Decrement();
                        stack.Push((currentPair, PairState.Filled));
                        currentPair = (Pair)currentPair.Right;
                        state = PairState.FillingLeft;
                    }
                }
                else if (char.IsDigit(this.Number[i]))
                {
                    if (state == PairState.FillingLeft)
                    {
                        currentPair.Left = new ValueItem(int.Parse(this.Number[i].ToString()));
                        state = PairState.FillingRight;
                        unresolved.Decrement();
                    }
                    else
                    {
                        currentPair.Right = new ValueItem(int.Parse(this.Number[i].ToString()));
                        state = PairState.Filled;
                        unresolved.Decrement();
                    }
                }
                else if (this.Number[i] == ',')
                {
                    state = PairState.FillingRight;
                }
                else if (this.Number[i] == ']')
                {
                    state = PairState.Filled;
                    if (stack.Count > 0)
                    {
                        (currentPair, state) = stack.Pop();
                    }
                }
            }

            unresolved.Counter.Should().Be(0);
            return top.GetMagnitude();
        }

        internal void Add(SnailFishNumber other)
        {
            this.number = $"[{this.number},{other.Number}]";
            this.Reduce();
        }

        internal void Add(string snailFishNumberString)
        {
            this.number = $"[{this.number},{snailFishNumberString}]";
            this.Reduce();
        }

        internal void Reduce()
        {
            ((int left, int right), ReductionType action) = this.FindReduce();
            while (action != ReductionType.None)
            {
                this.number = action switch
                {
                    ReductionType.Explode => this.Explode((left, right)),
                    ReductionType.Split => this.Split(left),
                    _ => throw new InvalidDataException("Unexpected type in switch"),
                };

                ((left, right), action) = this.FindReduce();
            }
        }
    }
}

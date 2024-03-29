﻿// -----------------------------------------------------------------------
// <copyright file="UtilityFunctions.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
    using System.Collections;
    using System.Text;

    /// <summary>
    /// Holds utility functions.
    /// </summary>
    internal static class UtilityFunctions
    {
        /// <summary>
        /// Traverse an enumerable with a sliding window, and run an aggregate on the windows.
        ///
        /// Say there are five elements: A, B, C, D, E and the window size is 3.
        /// This will yield windows A, B, C and B, C, D and C, D, E. So the aggregate will be computed 3 times.
        /// </summary>
        /// <typeparam name="T">Generic type of the Enumerable. Typically it would be numeric, since we will calculate an aggregate.</typeparam>
        /// <param name="source">The original collection.</param>
        /// <param name="windowSize">THe windows size.</param>
        /// <param name="aggregateFunction">An aggregate functio to execute over the windows.</param>
        /// <returns>A windows enumerable across the source enumerable.</returns>
        internal static IEnumerable<T> WindowedAggregation<T>(this IEnumerable<T> source, int windowSize, Func<T, T, T> aggregateFunction)
        {
            IEnumerator<T> enumerator = source.GetEnumerator();
            /*
             * We accumulate the window by pushes onto the end of this list. When a window has been processed,
             * the first element is dropped off to make room for the next window.
             */
            List<T> windowValues = new();
            if (!enumerator.MoveNext())
            {
                yield break;
            }

            for (int i = 0; i < windowSize - 1; i++)
            {
                windowValues.Add(enumerator.Current);
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
            }

            while (true)
            {
                windowValues.Add(enumerator.Current);
                var agg = windowValues.Aggregate(aggregateFunction);
                yield return agg;
                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                windowValues.RemoveAt(0);
            }
        }

        internal static IEnumerable<List<T>> ProduceWindows<T>(this IEnumerable<T> source, int windowSize)
        {
            IEnumerator<T> enumerator = source.GetEnumerator();
            /*
             * We accumulate the window by pushes onto the end of this list. When a window has been processed,
             * the first element is dropped off to make room for the next window.
             */
            List<T> windowValues = new();
            if (!enumerator.MoveNext())
            {
                yield break;
            }

            for (int i = 0; i < windowSize - 1; i++)
            {
                windowValues.Add(enumerator.Current);
                if (!enumerator.MoveNext())
                {
                    yield break;
                }
            }

            while (true)
            {
                windowValues.Add(enumerator.Current);
                yield return new List<T>(windowValues);
                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                windowValues.RemoveAt(0);
            }
        }

        internal static string SortString(this string source)
        {
            char[] outputChars = source.ToCharArray();
            Array.Sort(outputChars);
            return new string(outputChars);
        }

        internal static IEnumerable<T> Flatten<T>(this T[,] source)
        {
            for (int row = 0; row < source.GetLength(0); row++)
            {
                for (int col = 0; col < source.GetLength(1); col++)
                {
                    yield return source[row, col];
                }
            }
        }

        internal static BitArray CopyBottomBits(this BitArray source, int bitCountToCopy)
        {
            if (source.Length < bitCountToCopy)
            {
                throw new ArgumentException($"Passed in bit array has ${source.Length} bits, but copy amount requested is {bitCountToCopy}");
            }

            BitArray target = new(bitCountToCopy);
            for (int i = 0; i < bitCountToCopy; i++)
            {
                target[i] = source[i];
            }

            return target;
        }

        internal static string Print(this BitArray source)
        {
            var sb = new StringBuilder();
            for (int i = source.Length - 1; i >= 0; i--)
            {
                sb.Append(source[i] ? "1" : "0");
            }

            return sb.ToString();
        }
    }
}
// -----------------------------------------------------------------------
// <copyright file="UtilityFunctions.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
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
        internal static IEnumerable<T> WindowedTraverse<T>(this IEnumerable<T> source, int windowSize, Func<T, T, T> aggregateFunction)
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
    }
}
// -----------------------------------------------------------------------
// <copyright file="UtilityFunctions.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
    internal static class UtilityFunctions
    {
        internal static IEnumerable<T> WindowTraverse<T>(this IEnumerable<T> toBeProcessed, int windowSize, Func<T, T, T> aggregateFunction)
        {
            IEnumerator<T> enumerator = toBeProcessed.GetEnumerator();
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
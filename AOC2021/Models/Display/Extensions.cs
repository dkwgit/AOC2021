// -----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    using System.Linq;

    internal static class Extensions
    {
        internal static (IDigit D, int Index) FindSegmentIntersectionWith(this IEnumerable<(IDigit Digit, int Index)> source, IDigit intersectWithMe)
        {
            return source.Where(x => intersectWithMe.Segments.All(c => x.Digit.Segments.Any(a => a == c))).Single();
        }

        internal static Display ToProduce(this (IDigit Digit, int Index) source, int number)
        {
            Display display = source.Digit.Display;
            IDigit current = display.Digits[source.Index];
            display.Digits[source.Index] = new Digit(display, current.Signal, number);

            return display;
        }

        internal static (IDigit D, int Index) FindDigitBySingleSegment(this IEnumerable<(IDigit Digit, int Index)> source, char segment)
        {
            return source.Where(x => x.Digit.Segments.Any(a => a == segment)).Single();
        }

        internal static IEnumerable<char> IntersectSegments(this IDigit source, IDigit target)
        {
            return source.Segments.Intersect(target.Segments);
        }
    }
}

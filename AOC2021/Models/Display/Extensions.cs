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
            display.Digits[source.Index] = number switch
            {
                3 => new Three(display, current.Signal),
                9 => new Nine(display, current.Signal),
                0 => new Zero(display, current.Signal),
                6 => new Six(display, current.Signal),
                5 => new Five(display, current.Signal),
                2 => new Two(display, current.Signal),
                _ => throw new InvalidDataException(),
            };
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

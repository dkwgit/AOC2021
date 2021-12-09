// -----------------------------------------------------------------------
// <copyright file="IDigit.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal interface IDigit
    {
        int Digit { get; }

        string Signal { get; }

        Display Display { get; }

        int Length { get; }

        char[] Segments { get; }
    }
}

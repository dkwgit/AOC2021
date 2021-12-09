// -----------------------------------------------------------------------
// <copyright file="Two.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal class Two : Digit, IDigit
    {
        internal Two(Display display, string signal)
            : base(display, signal, 2)
        {
        }
    }
}
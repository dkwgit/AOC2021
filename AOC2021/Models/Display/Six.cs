// -----------------------------------------------------------------------
// <copyright file="Six.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal class Six : Digit, IDigit
    {
        internal Six(Display display, string signal)
           : base(display, signal, 6)
        {
        }
    }
}

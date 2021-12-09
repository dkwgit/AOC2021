// -----------------------------------------------------------------------
// <copyright file="Seven.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal class Seven : Digit, IDigit
    {
        internal Seven(Display display, string signal)
           : base(display, signal, 7)
        {
        }
    }
}

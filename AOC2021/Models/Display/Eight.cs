// -----------------------------------------------------------------------
// <copyright file="Eight.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal class Eight : Digit, IDigit
    {
        internal Eight(Display display, string signal)
           : base(display, signal, 8)
        {
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="Nine.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal class Nine : Digit, IDigit
    {
        internal Nine(Display display, string signal)
           : base(display, signal, 9)
        {
        }
    }
}
